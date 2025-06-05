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

        public Form1()
        {
            InitializeComponent();
            LoadGroups();
        }

        private void LoadGroups()
        {
            flowGroupButtons.Controls.Clear();

            if (!File.Exists("menu.db"))
            {
                return;
            }

            using (var con = new SQLiteConnection(dbPath))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT DISTINCT Grup FROM Menu", con);
                var reader = cmd.ExecuteReader();

                bool firstGroupLoaded = false;

                while (reader.Read())
                {
                    string group = reader["Grup"]?.ToString() ?? string.Empty;

                    // Hitung panjang tombol berdasarkan jumlah huruf (misal: 10px per huruf + padding minimal 30px)
                    int charWidth = 10;
                    int minWidth = 40;
                    int btnWidth = Math.Max(minWidth, group.Length * charWidth + 30);

                    var btn = new Button()
                    {
                        Text = group,
                        ForeColor = Color.White,
                        Size = new Size(btnWidth, 40), // lebar dinamis, tinggi tetap
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        Tag = group,
                        FlatStyle = FlatStyle.Flat,
                    };
                    btn.FlatAppearance.BorderSize = 0;

                    btn.Click += (s, e) =>
                    {
                        selectedGroup = group;
                        LoadApps(group);
                    };

                    flowGroupButtons.Controls.Add(btn);

                    // Load otomatis grup pertama
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

            var imageList = new ImageList
            {
                ImageSize = new Size(32, 32),
                ColorDepth = ColorDepth.Depth32Bit
            };

            using (var con = new SQLiteConnection(dbPath))
            {
                con.Open();
                var cmd = new SQLiteCommand("SELECT Nama, Path FROM Menu WHERE Grup = @g", con);
                cmd.Parameters.AddWithValue("@g", group);
                var reader = cmd.ExecuteReader();
                int index = 0;
                while (reader.Read())
                {
                    string nama = reader["Nama"]?.ToString() ?? string.Empty;
                    string path = reader["Path"]?.ToString() ?? string.Empty;
                    Bitmap bmp = ExtractAppIcon(path);
                    imageList.Images.Add(bmp);
                    allApps.Add(new AppItem { Name = nama, Path = path, ImageIndex = index++ });
                }
            }

            listViewApps.LargeImageList = imageList;
            FilterApps(searchBox.Text);
            listViewApps.EndUpdate();
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
        private Bitmap ExtractAppIcon(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return SystemIcons.Warning.ToBitmap();
                IntPtr[] largeIcons = new IntPtr[1];
                int count = ExtractIconEx(path, 0, largeIcons, null, 1);
                if (count > 0 && largeIcons[0] != IntPtr.Zero)
                {
                    using (Icon icon = Icon.FromHandle(largeIcons[0]))
                    {
                        return new Bitmap(icon.ToBitmap(), new Size(32, 32));
                    }
                }
                Icon assocIcon = Icon.ExtractAssociatedIcon(path);
                if (assocIcon != null)
                    return new Bitmap(assocIcon.ToBitmap(), new Size(32, 32));
            }
            catch
            {
                // Ignore and return fallback
            }
            return SystemIcons.WinLogo.ToBitmap();
        }

        private void listViewApps_DoubleClick(object sender, EventArgs e)
        {
            if (listViewApps.SelectedItems.Count > 0)
            {
                string path = listViewApps.SelectedItems[0].Tag?.ToString();
                if (!string.IsNullOrEmpty(path))
                {
                    if (Path.GetExtension(path).Equals(".url", StringComparison.OrdinalIgnoreCase))
                    {
                        // Buka file .url menggunakan default browser
                        try
                        {
                            string url = GetUrlFromInternetShortcut(path);
                            if (!string.IsNullOrEmpty(url))
                                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                            else
                                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                        }
                        catch
                        {
                            // fallback: buka file .url langsung
                            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                        }
                    }
                    else
                    {
                        Process.Start(path);
                    }
                }
            }
        }

        // Fungsi untuk membaca URL dari file .url (Internet Shortcut)
        private string GetUrlFromInternetShortcut(string filePath)
        {
            try
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    if (line.StartsWith("URL=", StringComparison.OrdinalIgnoreCase))
                        return line.Substring(4).Trim();
                }
            }
            catch { }
            return string.Empty;
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            FilterApps(searchBox.Text);
        }

        private void ButtonMinimaze_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ButtonMaximaze_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
        public AppItem()
        {
            Name = string.Empty;
            Path = string.Empty;
            ImageIndex = 0;
        }
    }
}