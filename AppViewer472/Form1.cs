namespace AppViewer472
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.IO;
    public partial class Form1 : Form
    {
        string dbPath = "Data Source=menu.db;Version=3;";
        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
        public static extern int ExtractIconEx(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, int nIcons);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        private List<AppItem> allApps = new List<AppItem>();
        private string selectedGroup = string.Empty;
        private ContextMenuStrip listViewContextMenu;
        private ToolStripMenuItem openFileLocationMenuItem;
        private ToolStripMenuItem viewMenuItem;
        private ToolStripMenuItem viewLargeIconMenuItem;
        private ToolStripMenuItem viewSmallIconMenuItem;
        private ToolStripMenuItem viewListMenuItem;
        private ToolStripMenuItem viewTileMenuItem;
        public Form1()
        {
            InitializeComponent();
            InitializeListViewContextMenu();
            LoadGroups();
        }
        private void InitializeListViewContextMenu()
        {
            listViewContextMenu = new ContextMenuStrip();
            openFileLocationMenuItem = new ToolStripMenuItem("Open File Location");
            openFileLocationMenuItem.Click += OpenFileLocationMenuItem_Click;
            viewMenuItem = new ToolStripMenuItem("View");
            viewLargeIconMenuItem = new ToolStripMenuItem("Large Icon");
            viewSmallIconMenuItem = new ToolStripMenuItem("Small Icon");
            viewListMenuItem = new ToolStripMenuItem("List");
            viewTileMenuItem = new ToolStripMenuItem("Tile");
            viewLargeIconMenuItem.Click += (s, e) => listViewApps.View = View.LargeIcon;
            viewSmallIconMenuItem.Click += (s, e) => listViewApps.View = View.SmallIcon;
            viewListMenuItem.Click += (s, e) => listViewApps.View = View.List;
            viewTileMenuItem.Click += (s, e) => listViewApps.View = View.Tile;
            viewMenuItem.DropDownItems.AddRange(new ToolStripItem[] { viewLargeIconMenuItem, viewSmallIconMenuItem, viewListMenuItem, viewTileMenuItem });
            listViewContextMenu.Items.Add(openFileLocationMenuItem);
            listViewContextMenu.Items.Add(viewMenuItem);
            listViewApps.ContextMenuStrip = listViewContextMenu;
            listViewApps.MouseUp += ListViewApps_MouseUp;
        }
        private void ListViewApps_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = listViewApps.FocusedItem;
                openFileLocationMenuItem.Enabled = focusedItem != null && focusedItem.Bounds.Contains(e.Location);
                listViewContextMenu.Show(listViewApps, e.Location);
            }
        }
        private void OpenFileLocationMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewApps.SelectedItems.Count > 0)
            {
                string path = listViewApps.SelectedItems[0].Tag?.ToString();
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    string folder = Path.GetDirectoryName(path);
                    if (!string.IsNullOrEmpty(folder))
                        Process.Start("explorer.exe", $"\"{folder}\"");
                }
            }
        }
        private void LoadGroups()
        {
            flowGroupButtons.Controls.Clear();
            if (!File.Exists("menu.db")) return;
            using (var con = new SQLiteConnection(dbPath))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT DISTINCT Grup FROM Menu", con);
                var reader = cmd.ExecuteReader();
                bool firstGroupLoaded = false;
                while (reader.Read())
                {
                    string group = reader["Grup"]?.ToString() ?? string.Empty;
                    int charWidth = 10, minWidth = 40, btnWidth = Math.Max(minWidth, group.Length * charWidth + 30);
                    var btn = new Button()
                    {
                        Text = group,
                        ForeColor = Color.White,
                        Size = new Size(btnWidth, 40),
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        Tag = group,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.Transparent
                    };
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 100, 150);
                    btn.Paint += (s, e) =>
                    {
                        if (selectedGroup == group)
                        {
                            var g = e.Graphics;
                            var rect = btn.ClientRectangle;
                            int padding = 4, diameterW = rect.Width - 2 * padding, diameterH = rect.Height - 2 * padding;
                            using (var brush = new SolidBrush(Color.FromArgb(80, 12, 220, 208)))
                            {
                                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                g.FillEllipse(brush, padding, padding, diameterW, diameterH);
                            }
                            TextRenderer.DrawText(g, btn.Text, btn.Font, rect, btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                        }
                    };
                    btn.MouseEnter += (s, e) => { if (selectedGroup != group) btn.BackColor = Color.FromArgb(40, 80, 140); };
                    btn.MouseLeave += (s, e) => { if (selectedGroup != group) btn.BackColor = Color.Transparent; };
                    btn.Click += (s, e) =>
                    {
                        selectedGroup = group;
                        LoadApps(group);
                        foreach (Control c in flowGroupButtons.Controls) c.Invalidate();
                    };
                    flowGroupButtons.Controls.Add(btn);
                    if (!firstGroupLoaded)
                    {
                        selectedGroup = group;
                        LoadApps(group);
                        firstGroupLoaded = true;
                    }
                }
            }
        }
        private void LoadApps(string group)
        {
            listViewApps.BeginUpdate();
            listViewApps.Clear();
            allApps.Clear();
            if (!File.Exists("menu.db"))
            {
                listViewApps.EndUpdate();
                return;
            }
            var imageList = new ImageList { ImageSize = new Size(32, 32), ColorDepth = ColorDepth.Depth32Bit };
            var smallImageList = new ImageList { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };
            using (var con = new SQLiteConnection(dbPath))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT Nama, Path, IconPath FROM Menu WHERE Grup = @g", con);
                cmd.Parameters.AddWithValue("@g", group);
                var reader = cmd.ExecuteReader();
                int index = 0;
                while (reader.Read())
                {
                    string nama = reader["Nama"]?.ToString() ?? string.Empty;
                    string path = reader["Path"]?.ToString() ?? string.Empty;
                    string iconPath = reader["IconPath"]?.ToString();
                    Bitmap bmp = ExtractAppIcon(path, iconPath);
                    imageList.Images.Add(bmp);
                    Bitmap smallBmp = new Bitmap(bmp, new Size(16, 16));
                    smallImageList.Images.Add(smallBmp);
                    allApps.Add(new AppItem { Name = nama, Path = path, ImageIndex = index++ });
                }
            }
            listViewApps.LargeImageList = imageList;
            listViewApps.SmallImageList = smallImageList;
            FilterApps(searchBox.Text);
            listViewApps.EndUpdate();
        }
        private Bitmap ExtractAppIcon(string path, string iconPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(iconPath) && File.Exists(iconPath))
                {
                    using (Icon icon = new Icon(iconPath))
                        return new Bitmap(icon.ToBitmap(), new Size(32, 32));
                }
                if (!File.Exists(path))
                    return SystemIcons.Warning.ToBitmap();
                IntPtr[] largeIcons = new IntPtr[1];
                int count = ExtractIconEx(path, 0, largeIcons, null, 1);
                if (count > 0 && largeIcons[0] != IntPtr.Zero)
                {
                    using (Icon icon = Icon.FromHandle(largeIcons[0]))
                        return new Bitmap(icon.ToBitmap(), new Size(32, 32));
                }
                Icon assocIcon = Icon.ExtractAssociatedIcon(path);
                if (assocIcon != null)
                    return new Bitmap(assocIcon.ToBitmap(), new Size(32, 32));
            }
            catch { }
            return SystemIcons.WinLogo.ToBitmap();
        }
        private void listViewApps_DoubleClick(object sender, EventArgs e)
        {
            if (listViewApps.SelectedItems.Count > 0)
            {
                string path = listViewApps.SelectedItems[0].Tag?.ToString();
                if (!string.IsNullOrEmpty(path))
                {
                    string ext = Path.GetExtension(path).ToLowerInvariant();
                    if (ext == ".url")
                    {
                        try
                        {
                            string url = GetUrlFromInternetShortcut(path);
                            if (!string.IsNullOrEmpty(url))
                            {
                                // Deteksi jika url adalah steam:// atau protocol lain
                                if (url.StartsWith("steam://", StringComparison.OrdinalIgnoreCase))
                                {
                                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                                }
                                else if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                                {
                                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                                }
                                else
                                {
                                    Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                                }
                            }
                            else
                            {
                                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                            }
                        }
                        catch
                        {
                            // fallback: buka file .url dengan shell
                            try { Process.Start(new ProcessStartInfo(path) { UseShellExecute = true }); } catch { }
                        }
                    }
                    else
                    {
                        try
                        {
                            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                        }
                        catch { }
                    }
                }
            }
        }
        private string GetUrlFromInternetShortcut(string filePath)
        {
            try
            {
                foreach (var line in File.ReadAllLines(filePath))
                    if (line.StartsWith("URL=", StringComparison.OrdinalIgnoreCase))
                        return line.Substring(4).Trim();
            }
            catch { }
            return string.Empty;
        }
        private void FilterApps(string keyword)
        {
            listViewApps.BeginUpdate();
            listViewApps.Items.Clear();
            foreach (var app in allApps)
            {
                if (app.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var item = new ListViewItem(app.Name);
                    item.Tag = app.Path;
                    item.ImageIndex = app.ImageIndex;
                    listViewApps.Items.Add(item);
                }
            }
            listViewApps.EndUpdate();
        }
        private void searchBox_TextChanged(object sender, EventArgs e) { FilterApps(searchBox.Text); }
        private void ButtonMinimaze_Click(object sender, EventArgs e) { this.WindowState = FormWindowState.Minimized; }
        private void ButtonMaximaze_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }
        private void ButtonClose_Click(object sender, EventArgs e) { this.Close(); }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
    public class AppItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int ImageIndex { get; set; }
        public AppItem() { Name = string.Empty; Path = string.Empty; ImageIndex = 0; }
    }
}
