using System;
using System.Data.SQLite;
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
        public Form1()
        {
            InitializeComponent();
            AllowDrop = true;
            DragEnter += FormAppEditor_DragEnter;
            DragDrop += FormAppEditor_DragDrop;
            AppListBox.SelectedIndexChanged += AppListBox_SelectedIndexChanged;
            AppButtonDelete.Click += AppButtonDelete_Click;
            InitDatabase();
            LoadGroups();
            AppListBox.LargeImageList = new ImageList { ImageSize = new Size(32, 32), ColorDepth = ColorDepth.Depth32Bit };
            AppListBox.SmallImageList = new ImageList { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };
            AppListBox.View = View.LargeIcon;
        }
        private void InitDatabase()
        {
            using (var con = new SQLiteConnection(dbPath))
            {
                con.Open();
                var cmd = new SQLiteCommand(@"
                CREATE TABLE IF NOT EXISTS Menu (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT, -- Tambahkan AUTOINCREMENT untuk ID unik
                    Nama TEXT,
                    Path TEXT,
                    Grup TEXT
                );", con);
                cmd.ExecuteNonQuery();
            }
        }
        private void FormAppEditor_DragEnter(object sender, DragEventArgs e)
        {

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
                if (Path.GetExtension(path)?.ToLower() == ".lnk")
                    path = ResolveShortcut(path);
                string nama = Path.GetFileNameWithoutExtension(path);
                string grup = GroupComboBox.SelectedItem?.ToString() ?? "General";
                using (var con = new SQLiteConnection(dbPath))
                {
                    con.Open();
                    var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM Menu WHERE Nama = @n AND Grup = @g", con);
                    checkCmd.Parameters.AddWithValue("@n", nama);
                    checkCmd.Parameters.AddWithValue("@g", grup);
                    long count = (long)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Aplikasi sudah ditambahkan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        continue;
                    }
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
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string path = ofd.FileName;
                    //if (Path.GetExtension(path)?.ToLower() == ".lnk")
                    //    path = ResolveShortcut(path);
                    string nama = Path.GetFileNameWithoutExtension(path);
                    string grup = GroupComboBox.SelectedItem?.ToString() ?? "General";
                    // Cek apakah nama aplikasi sudah ada di grup yang sama
                    using (var con = new SQLiteConnection(dbPath))
                    {
                        con.Open();
                        var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM Menu WHERE Nama = @n AND Grup = @g", con);
                        checkCmd.Parameters.AddWithValue("@n", nama);
                        checkCmd.Parameters.AddWithValue("@g", grup);
                        long count = (long)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Aplikasi sudah ditambahkan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
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
            using (var con = new SQLiteConnection(dbPath))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT Id, Nama, Path FROM Menu WHERE Grup = @g", con);
                cmd.Parameters.AddWithValue("@g", group);
                using (var reader = cmd.ExecuteReader())
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        string nama = reader["Nama"]?.ToString() ?? "Unknown App";
                        string path = reader["Path"]?.ToString();
                        int id = Convert.ToInt32(reader["Id"]);
                        if (string.IsNullOrEmpty(path))
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
            if (!System.IO.File.Exists(path))
            {
                return null;
            }
            IntPtr[] icons = new IntPtr[1];
            int count;
            if (largeIcon)
            {
                count = ExtractIconEx(path, 0, icons, null, 1);
            }
            else
            {
                count = ExtractIconEx(path, 0, null, icons, 1);
            }
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
                        if (groupName != null)
                        {
                            GroupComboBox.Items.Add(groupName);
                        }
                    }
                }
            }
        }
        private void GroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GroupComboBox.SelectedItem != null)
            {
                LoadAppsByGroup(GroupComboBox.SelectedItem.ToString());
            }
            else
            {
                AppListBox.Items.Clear();
                AppNameTextBox.Text = "";
                PathNameTextBox.Text = "";
            }
        }
        private void AppListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AppListBox.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = AppListBox.SelectedItems[0];
                string appName = selectedItem.Text;
                int appId = (int)(selectedItem.Tag ?? -1);
                string appPath = null;
                string grup = GroupComboBox.SelectedItem?.ToString() ?? "General";
                using (var con = new SQLiteConnection(dbPath))
                {
                    con.Open();
                    var cmd = new SQLiteCommand("SELECT Path FROM Menu WHERE Id = @id AND Nama = @n AND Grup = @g", con);
                    cmd.Parameters.AddWithValue("@id", appId);
                    cmd.Parameters.AddWithValue("@n", appName);
                    cmd.Parameters.AddWithValue("@g", grup);
                    appPath = cmd.ExecuteScalar()?.ToString();
                }
                AppNameTextBox.Text = appName;
                PathNameTextBox.Text = appPath ?? "";
            }
            else
            {
                AppNameTextBox.Text = "";
                PathNameTextBox.Text = "";
            }
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
            }
        }
        private void AppButtonDelete_Click(object sender, EventArgs e)
        {
            if (AppListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Pilih aplikasi yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem selectedItem = AppListBox.SelectedItems[0];
            int appId = (int)(selectedItem.Tag ?? -1);
            string grup = GroupComboBox.SelectedItem?.ToString() ?? "";

            if (MessageBox.Show("Yakin ingin menghapus aplikasi ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var con = new SQLiteConnection(dbPath))
                {
                    con.Open();
                    var cmd = new SQLiteCommand("DELETE FROM Menu WHERE Id = @id AND Grup = @g", con);
                    cmd.Parameters.AddWithValue("@id", appId);
                    cmd.Parameters.AddWithValue("@g", grup);
                    cmd.ExecuteNonQuery();
                }
                LoadAppsByGroup(grup);
            }
        }
    }
}