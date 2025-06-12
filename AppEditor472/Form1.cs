using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace AppEditor472
{
    public partial class Form1 : Form
    {
        private readonly string dbPath = "Data Source=menu.db;Version=3;";
        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
        public static extern int ExtractIconEx(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, int nIcons);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool DestroyIcon(IntPtr handle);
        private ContextMenuStrip appListContextMenu;
        private ToolStripMenuItem moveMenuItem;
        private ToolStripMenuItem viewMenuItem;
        private ToolStripMenuItem openFileLocationMenuItem;
        private ToolStripMenuItem changeIconMenuItem;
        private ToolStripMenuItem deleteAppMenuItem;
        private Dictionary<int, string> customIconPaths = new Dictionary<int, string>();
        private void InitializeAppListContextMenu()
        {
            appListContextMenu = new ContextMenuStrip();
            moveMenuItem = new ToolStripMenuItem("Move");
            appListContextMenu.Items.Add(moveMenuItem);
            viewMenuItem = new ToolStripMenuItem("View");
            viewMenuItem.DropDownItems.Add("Large Icon", null, (s, e) => AppListBox.View = View.LargeIcon);
            viewMenuItem.DropDownItems.Add("Small Icon", null, (s, e) => AppListBox.View = View.SmallIcon);
            viewMenuItem.DropDownItems.Add("List", null, (s, e) => AppListBox.View = View.List);
            viewMenuItem.DropDownItems.Add("Tile", null, (s, e) => AppListBox.View = View.Tile);
            appListContextMenu.Items.Add(viewMenuItem);
            openFileLocationMenuItem = new ToolStripMenuItem("Open File Location", null, OpenFileLocation_Click);
            appListContextMenu.Items.Add(openFileLocationMenuItem);
            changeIconMenuItem = new ToolStripMenuItem("Change Icon", null, ChangeIconMenuItem_Click);
            appListContextMenu.Items.Add(changeIconMenuItem);
            deleteAppMenuItem = new ToolStripMenuItem("Delete App", null, ContextMenuDelete_Click);
            appListContextMenu.Items.Add(deleteAppMenuItem);
            AppListBox.MouseUp += AppListBox_MouseUp;
        }
        private void AppListBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var item = AppListBox.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    moveMenuItem.DropDownItems.Clear();
                    foreach (var group in GroupComboBox.Items)
                    {
                        string groupName = group.ToString();
                        if (GroupComboBox.SelectedItem != null && groupName == GroupComboBox.SelectedItem.ToString())
                            continue;
                        var moveToGroupItem = new ToolStripMenuItem(groupName, null, (s, ev) => MoveAppToGroup(item, groupName));
                        moveMenuItem.DropDownItems.Add(moveToGroupItem);
                    }
                    moveMenuItem.Enabled = true;
                    openFileLocationMenuItem.Enabled = true;
                    changeIconMenuItem.Enabled = true;
                    deleteAppMenuItem.Enabled = true;
                }
                else
                {
                    moveMenuItem.DropDownItems.Clear();
                    moveMenuItem.Enabled = false;
                    openFileLocationMenuItem.Enabled = false;
                    changeIconMenuItem.Enabled = false;
                    deleteAppMenuItem.Enabled = false;
                }
                appListContextMenu.Show(AppListBox, e.Location);
            }
        }
        private void MoveAppToGroup(ListViewItem item, string targetGroup)
        {
            int appId = (int)(item.Tag ?? -1);
            if (appId == -1) return;
            using (var con = new SQLiteConnection(dbPath))
            {
                con.Open();
                var cmd = new SQLiteCommand("UPDATE Menu SET Grup = @g WHERE Id = @id", con);
                cmd.Parameters.AddWithValue("@g", targetGroup);
                cmd.Parameters.AddWithValue("@id", appId);
                cmd.ExecuteNonQuery();
            }
            LoadAppsByGroup(GroupComboBox.SelectedItem?.ToString() ?? "General");
        }
        private void OpenFileLocation_Click(object sender, EventArgs e)
        {
            if (AppListBox.SelectedItems.Count == 0) return;
            ListViewItem selectedItem = AppListBox.SelectedItems[0];
            int appId = (int)(selectedItem.Tag ?? -1);
            string appPath = null;
            string grup = GroupComboBox.SelectedItem?.ToString() ?? "General";
            using (var con = new SQLiteConnection(dbPath))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT Path FROM Menu WHERE Id = @id AND Grup = @g", con);
                cmd.Parameters.AddWithValue("@id", appId);
                cmd.Parameters.AddWithValue("@g", grup);
                appPath = cmd.ExecuteScalar()?.ToString();
            }
            if (!string.IsNullOrEmpty(appPath) && System.IO.File.Exists(appPath))
            {
                Process.Start("explorer.exe", $"/select,\"{appPath}\"");
            }
            else
            {
                MessageBox.Show("File tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Form1()
        {
            InitializeComponent();
            AllowDrop = true;
            DragEnter += FormAppEditor_DragEnter;
            DragDrop += FormAppEditor_DragDrop;
            AppListBox.SelectedIndexChanged += AppListBox_SelectedIndexChanged;
            AppButtonDelete.Click -= AppButtonDelete_Click;
            AppButtonDelete.Click += AppButtonDelete_Click;
            InitDatabase();
            LoadGroups();
            AppListBox.LargeImageList = new ImageList { ImageSize = new Size(32, 32), ColorDepth = ColorDepth.Depth32Bit };
            AppListBox.SmallImageList = new ImageList { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };
            AppListBox.View = View.LargeIcon;
            InitializeAppListContextMenu();
            this.Shown += (s, e) =>
            {
                if (GroupComboBox.SelectedItem != null)
                {
                    string selectedGroup = GroupComboBox.SelectedItem.ToString();
                    LoadAppsByGroup(selectedGroup);
                }
            };
        }
        private void InitDatabase()
        {
            using (var con = new SQLiteConnection(dbPath))
            {
                con.Open();
                var alterCmd = new SQLiteCommand("ALTER TABLE Menu ADD COLUMN IconPath TEXT", con);
                try { alterCmd.ExecuteNonQuery(); } catch { }
                var cmd = new SQLiteCommand(@"
                CREATE TABLE IF NOT EXISTS Menu (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nama TEXT,
                    Path TEXT,
                    Grup TEXT,
                    IconPath TEXT
                );", con);
                cmd.ExecuteNonQuery();
            }
        }
        private void FormAppEditor_DragEnter(object sender, DragEventArgs e)
        {
            if (GroupComboBox.SelectedItem == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        private void FormAppEditor_DragDrop(object sender, DragEventArgs e)
        {
            if (GroupComboBox.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih grup terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (e.Data == null) return;
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files == null) return;
            foreach (string file in files)
            {
                string path = file;
                string nama = Path.GetFileNameWithoutExtension(file);
                if (Path.GetExtension(path)?.ToLower() == ".lnk")
                    path = ResolveShortcut(path);
                string grup = GroupComboBox.SelectedItem?.ToString() ?? "General";
                using (var con = new SQLiteConnection(dbPath))
                {
                    con.Open();
                    var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM Menu WHERE Nama = @n AND Grup = @g", con);
                    checkCmd.Parameters.AddWithValue("@n", nama);
                    checkCmd.Parameters.AddWithValue("@g", grup);
                    long count = (long)checkCmd.ExecuteScalar();
                    if (count > 0) continue;
                    var cmd = new SQLiteCommand("INSERT INTO Menu (Nama, Path, Grup) VALUES (@n, @p, @g)", con);
                    cmd.Parameters.AddWithValue("@n", nama);
                    cmd.Parameters.AddWithValue("@p", path);
                    cmd.Parameters.AddWithValue("@g", grup);
                    cmd.ExecuteNonQuery();
                }
                LoadAppsByGroup(grup);
                AppNameTextBox.Text = "";
                PathNameTextBox.Text = "";
            }
        }
        private string ResolveShortcut(string shortcutPath)
        {
            var shell = new WshShell();
            var link = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            return link.TargetPath ?? shortcutPath;
        }
        private void AppButtonAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (GroupComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Silakan pilih grup terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ofd.Filter = "All files (*.*)|*.*";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string grup = GroupComboBox.SelectedItem?.ToString() ?? "General";
                    foreach (string path in ofd.FileNames)
                    {
                        string nama = Path.GetFileNameWithoutExtension(path);
                        using (var con = new SQLiteConnection(dbPath))
                        {
                            con.Open();
                            var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM Menu WHERE Nama = @n AND Grup = @g", con);
                            checkCmd.Parameters.AddWithValue("@n", nama);
                            checkCmd.Parameters.AddWithValue("@g", grup);
                            long count = (long)checkCmd.ExecuteScalar();
                            if (count > 0) continue;
                            var cmd = new SQLiteCommand("INSERT INTO Menu (Nama, Path, Grup) VALUES (@n, @p, @g)", con);
                            cmd.Parameters.AddWithValue("@n", nama);
                            cmd.Parameters.AddWithValue("@p", path);
                            cmd.Parameters.AddWithValue("@g", grup);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    LoadAppsByGroup(grup);
                    AppNameTextBox.Text = "";
                    PathNameTextBox.Text = "";
                }
            }
        }
        private void AppButtonSave_Click(object sender, EventArgs e)
        {
            if (AppListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Pilih aplikasi yang ingin diubah terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ListViewItem selectedItem = AppListBox.SelectedItems[0];
            string oldNama = selectedItem.Text;
            string newNama = AppNameTextBox.Text;
            string newPath = PathNameTextBox.Text;
            string grup = GroupComboBox.SelectedItem?.ToString() ?? "General";
            if (string.IsNullOrWhiteSpace(newNama) || string.IsNullOrWhiteSpace(newPath))
            {
                MessageBox.Show("Nama aplikasi dan Path tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var con = new SQLiteConnection(dbPath))
            {
                con.Open();
                var cmd = new SQLiteCommand("UPDATE Menu SET Nama = @newN, Path = @newP WHERE Nama = @oldN AND Grup = @g", con);
                cmd.Parameters.AddWithValue("@newN", newNama);
                cmd.Parameters.AddWithValue("@newP", newPath);
                cmd.Parameters.AddWithValue("@oldN", oldNama);
                cmd.Parameters.AddWithValue("@g", grup);
                cmd.ExecuteNonQuery();
            }
            LoadAppsByGroup(grup);
            MessageBox.Show("Perubahan berhasil disimpan!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void LoadAppsByGroup(string group)
        {
            AppListBox.Items.Clear();
            AppListBox.LargeImageList?.Images.Clear();
            AppListBox.SmallImageList?.Images.Clear();
            customIconPaths.Clear();
            using (var con = new SQLiteConnection(dbPath))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT Id, Nama, Path, IconPath FROM Menu WHERE Grup = @g", con);
                cmd.Parameters.AddWithValue("@g", group);
                using (var reader = cmd.ExecuteReader())
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        string nama = reader["Nama"]?.ToString() ?? "Unknown App";
                        string path = reader["Path"]?.ToString();
                        int id = Convert.ToInt32(reader["Id"]);
                        string iconPath = reader["IconPath"]?.ToString();
                        if (!string.IsNullOrEmpty(iconPath) && System.IO.File.Exists(iconPath))
                        {
                            customIconPaths[id] = iconPath;
                            try
                            {
                                Icon iconLarge = new Icon(iconPath, 32, 32);
                                AppListBox.LargeImageList?.Images.Add(iconLarge.ToBitmap());
                                Icon iconSmall = new Icon(iconPath, 16, 16);
                                AppListBox.SmallImageList?.Images.Add(iconSmall.ToBitmap());
                            }
                            catch
                            {
                                AppListBox.LargeImageList?.Images.Add(SystemIcons.WinLogo.ToBitmap());
                                AppListBox.SmallImageList?.Images.Add(SystemIcons.WinLogo.ToBitmap());
                            }
                        }
                        else if (string.IsNullOrEmpty(path))
                        {
                            AppListBox.LargeImageList?.Images.Add(SystemIcons.Question.ToBitmap());
                            AppListBox.SmallImageList?.Images.Add(SystemIcons.Question.ToBitmap());
                        }
                        else
                        {
                            Bitmap largeBmp = ExtractAppIcon(path, new Size(32, 32), true);
                            AppListBox.LargeImageList?.Images.Add(largeBmp ?? SystemIcons.WinLogo.ToBitmap());
                            Bitmap smallBmp = ExtractAppIcon(path, new Size(16, 16), false);
                            AppListBox.SmallImageList?.Images.Add(smallBmp ?? SystemIcons.WinLogo.ToBitmap());
                        }
                        var item = new ListViewItem(nama, i++);
                        item.Tag = id;
                        AppListBox.Items.Add(item);
                    }
                }
            }
        }
        private Bitmap ExtractAppIcon(string path, Size size, bool largeIcon)
        {
            if (!System.IO.File.Exists(path)) return null;
            IntPtr[] icons = new IntPtr[1];
            int count;
            if (largeIcon)
                count = ExtractIconEx(path, 0, icons, null, 1);
            else
                count = ExtractIconEx(path, 0, null, icons, 1);
            if (count > 0 && icons[0] != IntPtr.Zero)
            {
                try
                {
                    using (Icon icon = Icon.FromHandle(icons[0]))
                    {
                        return new Bitmap(icon.ToBitmap(), size);
                    }
                }
                finally
                {
                    DestroyIcon(icons[0]);
                }
            }
            return null;
        }
        private void LoadGroups()
        {
            GroupComboBox.Items.Clear();
            using (var con = new SQLiteConnection(dbPath))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT DISTINCT Grup FROM Menu", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string groupName = reader["Grup"]?.ToString();
                        if (!string.IsNullOrEmpty(groupName))
                            GroupComboBox.Items.Add(groupName);
                    }
                }
            }
            if (GroupComboBox.Items.Count > 0 && GroupComboBox.SelectedItem == null)
                GroupComboBox.SelectedIndex = 0;
        }
        private void GroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GroupComboBox.SelectedItem != null)
            {
                string selectedGroup = GroupComboBox.SelectedItem.ToString();
                LoadAppsByGroup(selectedGroup);
            }
            else
            {
                AppListBox.Items.Clear();
            }
            AppNameTextBox.Text = "";
            PathNameTextBox.Text = "";
        }
        private void GroupButtonAdd_Click(object sender, EventArgs e)
        {
            string newGroup = GroupTextBox.Text;
            if (!string.IsNullOrWhiteSpace(newGroup) && !GroupComboBox.Items.Contains(newGroup))
            {
                GroupComboBox.Items.Add(newGroup);
                GroupComboBox.SelectedItem = newGroup;
            }
        }
        private void GroupButtonDelete_Click(object sender, EventArgs e)
        {
            if (GroupComboBox.SelectedItem != null)
            {
                string group = GroupComboBox.SelectedItem.ToString();
                using (var con = new SQLiteConnection(dbPath))
                {
                    con.Open();
                    var cmd = new SQLiteCommand("DELETE FROM Menu WHERE Grup = @g", con);
                    cmd.Parameters.AddWithValue("@g", group);
                    cmd.ExecuteNonQuery();
                }
                GroupComboBox.Items.Remove(group);
                if (GroupComboBox.Items.Count > 0)
                    GroupComboBox.SelectedIndex = 0;
                else
                    AppListBox.Items.Clear();
                AppNameTextBox.Text = "";
                PathNameTextBox.Text = "";
            }
        }
        private void AppButtonDelete_Click(object sender, EventArgs e)
        {
            if (AppListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Pilih aplikasi yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string grup = GroupComboBox.SelectedItem?.ToString() ?? "";
            var ids = new List<int>();
            foreach (ListViewItem selectedItem in AppListBox.SelectedItems)
            {
                int appId = (int)(selectedItem.Tag ?? -1);
                if (appId != -1)
                    ids.Add(appId);
            }
            if (ids.Count > 0 && MessageBox.Show("Yakin ingin menghapus aplikasi terpilih?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var con = new SQLiteConnection(dbPath))
                {
                    con.Open();
                    foreach (int appId in ids)
                    {
                        var cmd = new SQLiteCommand("DELETE FROM Menu WHERE Id = @id AND Grup = @g", con);
                        cmd.Parameters.AddWithValue("@id", appId);
                        cmd.Parameters.AddWithValue("@g", grup);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadAppsByGroup(grup);
                AppNameTextBox.Text = "";
                PathNameTextBox.Text = "";
            }
        }
        private void ChangeIconMenuItem_Click(object sender, EventArgs e)
        {
            if (AppListBox.SelectedItems.Count == 0) return;
            ListViewItem selectedItem = AppListBox.SelectedItems[0];
            int appId = (int)(selectedItem.Tag ?? -1);
            string grup = GroupComboBox.SelectedItem?.ToString() ?? "General";
            string appPath = null;
            using (var con = new SQLiteConnection(dbPath))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT Path FROM Menu WHERE Id = @id AND Grup = @g", con);
                cmd.Parameters.AddWithValue("@id", appId);
                cmd.Parameters.AddWithValue("@g", grup);
                appPath = cmd.ExecuteScalar()?.ToString();
            }
            if (string.IsNullOrEmpty(appPath)) return;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Icon Files (*.ico)|*.ico";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string iconPath = ofd.FileName;
                    customIconPaths[appId] = iconPath;
                    using (var con = new SQLiteConnection(dbPath))
                    {
                        con.Open();
                        var cmd = new SQLiteCommand("UPDATE Menu SET IconPath = @icon WHERE Id = @id", con);
                        cmd.Parameters.AddWithValue("@icon", iconPath);
                        cmd.Parameters.AddWithValue("@id", appId);
                        cmd.ExecuteNonQuery();
                    }
                    try
                    {
                        Icon icon = new Icon(iconPath, 32, 32);
                        int idx = selectedItem.ImageIndex;
                        if (AppListBox.LargeImageList != null && idx >= 0 && idx < AppListBox.LargeImageList.Images.Count)
                            AppListBox.LargeImageList.Images[idx] = icon.ToBitmap();
                        icon = new Icon(iconPath, 16, 16);
                        if (AppListBox.SmallImageList != null && idx >= 0 && idx < AppListBox.SmallImageList.Images.Count)
                            AppListBox.SmallImageList.Images[idx] = icon.ToBitmap();
                        AppListBox.Refresh();
                    }
                    catch
                    {
                        MessageBox.Show("Gagal mengganti icon.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void ContextMenuDelete_Click(object sender, EventArgs e)
        {
            if (AppListBox.SelectedItems.Count == 0) return;
            string grup = GroupComboBox.SelectedItem?.ToString() ?? "";
            var ids = new List<int>();
            foreach (ListViewItem selectedItem in AppListBox.SelectedItems)
            {
                int appId = (int)(selectedItem.Tag ?? -1);
                if (appId != -1)
                    ids.Add(appId);
            }
            if (ids.Count > 0 && MessageBox.Show("Yakin ingin menghapus aplikasi terpilih?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var con = new SQLiteConnection(dbPath))
                {
                    con.Open();
                    foreach (int appId in ids)
                    {
                        var cmd = new SQLiteCommand("DELETE FROM Menu WHERE Id = @id AND Grup = @g", con);
                        cmd.Parameters.AddWithValue("@id", appId);
                        cmd.Parameters.AddWithValue("@g", grup);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadAppsByGroup(grup);
                AppNameTextBox.Text = "";
                PathNameTextBox.Text = "";
            }
        }
        private void AppListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AppListBox.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = AppListBox.SelectedItems[0];
                AppNameTextBox.Text = selectedItem.Text;
                int appId = (int)(selectedItem.Tag ?? -1);
                if (appId != -1)
                {
                    using (var con = new SQLiteConnection(dbPath))
                    {
                        con.Open();
                        var cmd = new SQLiteCommand("SELECT Path FROM Menu WHERE Id = @id", con);
                        cmd.Parameters.AddWithValue("@id", appId);
                        PathNameTextBox.Text = cmd.ExecuteScalar()?.ToString();
                    }
                }
            }
            else
            {
                AppNameTextBox.Text = "";
                PathNameTextBox.Text = "";
            }
        }
    }
}
