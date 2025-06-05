namespace AppViewer472
{
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using static System.Net.Mime.MediaTypeNames;

    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowGroupButtons;
        private System.Windows.Forms.ListView listViewApps;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelTopBar;
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.flowGroupButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.listViewApps = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.searchBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelTopBar = new System.Windows.Forms.Panel();
            this.labelGG = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.ButtonMaximaze = new System.Windows.Forms.Button();
            this.ButtonMinimaze = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelTopBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowGroupButtons
            // 
            this.flowGroupButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(70)))), ((int)(((byte)(120)))));
            this.flowGroupButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowGroupButtons.Location = new System.Drawing.Point(0, 0);
            this.flowGroupButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flowGroupButtons.Name = "flowGroupButtons";
            this.flowGroupButtons.Size = new System.Drawing.Size(621, 38);
            this.flowGroupButtons.TabIndex = 0;
            this.flowGroupButtons.WrapContents = false;
            this.flowGroupButtons.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // listViewApps
            // 
            this.listViewApps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewApps.BackColor = System.Drawing.Color.White;
            this.listViewApps.ForeColor = System.Drawing.Color.Black;
            this.listViewApps.HideSelection = false;
            this.listViewApps.LargeImageList = this.imageList1;
            this.listViewApps.Location = new System.Drawing.Point(0, 140);
            this.listViewApps.MultiSelect = false;
            this.listViewApps.Name = "listViewApps";
            this.listViewApps.Size = new System.Drawing.Size(784, 422);
            this.listViewApps.TabIndex = 1;
            this.listViewApps.UseCompatibleStateImageBehavior = false;
            this.listViewApps.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(2, 3);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(120, 20);
            this.searchBox.TabIndex = 2;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.Silver;
            this.searchButton.Location = new System.Drawing.Point(130, 1);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(30, 23);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "🔍";
            this.searchButton.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(70)))), ((int)(((byte)(120)))));
            this.panel1.Controls.Add(this.searchBox);
            this.panel1.Controls.Add(this.searchButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(621, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(163, 38);
            this.panel1.TabIndex = 4;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // panelTopBar
            // 
            this.panelTopBar.BackColor = System.Drawing.Color.Transparent;
            this.panelTopBar.Controls.Add(this.flowGroupButtons);
            this.panelTopBar.Controls.Add(this.panel1);
            this.panelTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopBar.Location = new System.Drawing.Point(0, 106);
            this.panelTopBar.Name = "panelTopBar";
            this.panelTopBar.Size = new System.Drawing.Size(784, 38);
            this.panelTopBar.TabIndex = 10;
            // 
            // labelGG
            // 
            this.labelGG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(61)))), ((int)(((byte)(97)))));
            this.labelGG.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelGG.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.labelGG.ForeColor = System.Drawing.Color.White;
            this.labelGG.Location = new System.Drawing.Point(0, 0);
            this.labelGG.Name = "labelGG";
            this.labelGG.Size = new System.Drawing.Size(784, 106);
            this.labelGG.TabIndex = 0;
            this.labelGG.Text = "AESMSIX-SSR";
            this.labelGG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelGG.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(97)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(784, 29);
            this.label1.TabIndex = 11;
            this.label1.Text = "Inspired By CYBERINDO";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(61)))), ((int)(((byte)(97)))));
            this.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ButtonClose.Location = new System.Drawing.Point(763, 0);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(20, 20);
            this.ButtonClose.TabIndex = 12;
            this.ButtonClose.Text = "X";
            this.ButtonClose.UseVisualStyleBackColor = false;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // ButtonMaximaze
            // 
            this.ButtonMaximaze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonMaximaze.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(61)))), ((int)(((byte)(97)))));
            this.ButtonMaximaze.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonMaximaze.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ButtonMaximaze.Location = new System.Drawing.Point(741, 0);
            this.ButtonMaximaze.Name = "ButtonMaximaze";
            this.ButtonMaximaze.Size = new System.Drawing.Size(20, 20);
            this.ButtonMaximaze.TabIndex = 13;
            this.ButtonMaximaze.Text = "☐";
            this.ButtonMaximaze.UseVisualStyleBackColor = false;
            this.ButtonMaximaze.Click += new System.EventHandler(this.ButtonMaximaze_Click);
            // 
            // ButtonMinimaze
            // 
            this.ButtonMinimaze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonMinimaze.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(61)))), ((int)(((byte)(97)))));
            this.ButtonMinimaze.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonMinimaze.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ButtonMinimaze.Location = new System.Drawing.Point(718, 0);
            this.ButtonMinimaze.Name = "ButtonMinimaze";
            this.ButtonMinimaze.Size = new System.Drawing.Size(20, 20);
            this.ButtonMinimaze.TabIndex = 14;
            this.ButtonMinimaze.Text = "─";
            this.ButtonMinimaze.UseVisualStyleBackColor = false;
            this.ButtonMinimaze.Click += new System.EventHandler(this.ButtonMinimaze_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.ButtonMinimaze);
            this.Controls.Add(this.ButtonMaximaze);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelTopBar);
            this.Controls.Add(this.labelGG);
            this.Controls.Add(this.listViewApps);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "App Launcher";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelTopBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private Label labelGG;
        private Label label1;
        private Button ButtonClose;
        private Button ButtonMaximaze;
        private Button ButtonMinimaze;
    }
}