namespace AppViewer472
{
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml.Linq; // This using directive seems unused in the provided Designer.cs, keeping it for consistency if it's used elsewhere.
    using static System.Net.Mime.MediaTypeNames; // This using directive seems unused in the provided Designer.cs, keeping it for consistency if it's used elsewhere.

    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowGroupButtons;
        private System.Windows.Forms.ListView listViewApps;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Panel panelTopBar; // Renamed from panel1 to be more descriptive of its role
        private System.Windows.Forms.Panel panelHeader; // New panel for the application header (title and control buttons)
        private System.Windows.Forms.Label labelGG;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.Button ButtonMaximaze;
        private System.Windows.Forms.Button ButtonMinimaze;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.flowGroupButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.listViewApps = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.searchBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.panelTopBar = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.ButtonMinimaze = new System.Windows.Forms.Button();
            this.ButtonMaximaze = new System.Windows.Forms.Button();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelGG = new System.Windows.Forms.Label();
            this.panelTopBar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowGroupButtons
            // 
            this.flowGroupButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(70)))), ((int)(((byte)(120)))));
            this.flowGroupButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowGroupButtons.Location = new System.Drawing.Point(0, 0);
            this.flowGroupButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flowGroupButtons.Name = "flowGroupButtons";
            this.flowGroupButtons.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.flowGroupButtons.Size = new System.Drawing.Size(784, 38);
            this.flowGroupButtons.TabIndex = 0;
            this.flowGroupButtons.WrapContents = false;
            this.flowGroupButtons.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // listViewApps
            // 
            this.listViewApps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewApps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.listViewApps.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewApps.ForeColor = System.Drawing.Color.Black;
            this.listViewApps.HideSelection = false;
            this.listViewApps.LargeImageList = this.imageList1;
            this.listViewApps.Location = new System.Drawing.Point(25, 166);
            this.listViewApps.MultiSelect = false;
            this.listViewApps.Name = "listViewApps";
            this.listViewApps.Size = new System.Drawing.Size(738, 374);
            this.listViewApps.TabIndex = 1;
            this.listViewApps.UseCompatibleStateImageBehavior = false;
            this.listViewApps.Click += new System.EventHandler(this.listViewApps_DoubleClick);
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
            this.searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBox.BackColor = System.Drawing.Color.CornflowerBlue;
            this.searchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchBox.ForeColor = System.Drawing.Color.White;
            this.searchBox.Location = new System.Drawing.Point(605, 9);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(120, 20);
            this.searchBox.TabIndex = 2;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.Location = new System.Drawing.Point(731, 7);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(30, 23);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "🔍";
            this.searchButton.UseVisualStyleBackColor = false;
            // 
            // panelTopBar
            // 
            this.panelTopBar.BackColor = System.Drawing.Color.Transparent;
            this.panelTopBar.Controls.Add(this.searchBox);
            this.panelTopBar.Controls.Add(this.searchButton);
            this.panelTopBar.Controls.Add(this.flowGroupButtons);
            this.panelTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopBar.Location = new System.Drawing.Point(0, 106);
            this.panelTopBar.Name = "panelTopBar";
            this.panelTopBar.Size = new System.Drawing.Size(784, 38);
            this.panelTopBar.TabIndex = 10;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(61)))), ((int)(((byte)(97)))));
            this.panelHeader.Controls.Add(this.ButtonMinimaze);
            this.panelHeader.Controls.Add(this.ButtonMaximaze);
            this.panelHeader.Controls.Add(this.ButtonClose);
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Controls.Add(this.labelGG);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(784, 106);
            this.panelHeader.TabIndex = 15;
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // ButtonMinimaze
            // 
            this.ButtonMinimaze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonMinimaze.BackColor = System.Drawing.Color.Transparent;
            this.ButtonMinimaze.FlatAppearance.BorderSize = 0;
            this.ButtonMinimaze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonMinimaze.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ButtonMinimaze.ForeColor = System.Drawing.Color.White;
            this.ButtonMinimaze.Location = new System.Drawing.Point(718, 0);
            this.ButtonMinimaze.Name = "ButtonMinimaze";
            this.ButtonMinimaze.Size = new System.Drawing.Size(22, 22);
            this.ButtonMinimaze.TabIndex = 14;
            this.ButtonMinimaze.Text = "─";
            this.ButtonMinimaze.UseVisualStyleBackColor = false;
            this.ButtonMinimaze.Click += new System.EventHandler(this.ButtonMinimaze_Click);
            // 
            // ButtonMaximaze
            // 
            this.ButtonMaximaze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonMaximaze.BackColor = System.Drawing.Color.Transparent;
            this.ButtonMaximaze.FlatAppearance.BorderSize = 0;
            this.ButtonMaximaze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonMaximaze.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ButtonMaximaze.ForeColor = System.Drawing.Color.White;
            this.ButtonMaximaze.Location = new System.Drawing.Point(741, 0);
            this.ButtonMaximaze.Name = "ButtonMaximaze";
            this.ButtonMaximaze.Size = new System.Drawing.Size(22, 22);
            this.ButtonMaximaze.TabIndex = 13;
            this.ButtonMaximaze.Text = "☐";
            this.ButtonMaximaze.UseVisualStyleBackColor = false;
            this.ButtonMaximaze.Click += new System.EventHandler(this.ButtonMaximaze_Click);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.BackColor = System.Drawing.Color.Transparent;
            this.ButtonClose.FlatAppearance.BorderSize = 0;
            this.ButtonClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.ButtonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ButtonClose.ForeColor = System.Drawing.Color.White;
            this.ButtonClose.Location = new System.Drawing.Point(764, 0);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(22, 22);
            this.ButtonClose.TabIndex = 12;
            this.ButtonClose.Text = "X";
            this.ButtonClose.UseVisualStyleBackColor = false;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(0, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(784, 26);
            this.label1.TabIndex = 11;
            this.label1.Text = "Inspired By CYBERINDO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // labelGG
            // 
            this.labelGG.BackColor = System.Drawing.Color.Transparent;
            this.labelGG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGG.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.labelGG.ForeColor = System.Drawing.Color.White;
            this.labelGG.Location = new System.Drawing.Point(0, 0);
            this.labelGG.Name = "labelGG";
            this.labelGG.Size = new System.Drawing.Size(784, 106);
            this.labelGG.TabIndex = 0;
            this.labelGG.Text = "AESMSIX-SSR";
            this.labelGG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelGG.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panelTopBar);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.listViewApps);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "App Launcher";
            this.panelTopBar.ResumeLayout(false);
            this.panelTopBar.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        // Existing private members, ensure they are declared if not already
        // private System.ComponentModel.IContainer components = null; // Already declared at the top
        // private System.Windows.Forms.FlowLayoutPanel flowGroupButtons; // Already declared at the top
        // private System.Windows.Forms.ListView listViewApps; // Already declared at the top
        // private System.Windows.Forms.ImageList imageList1; // Already declared at the top
        // private System.Windows.Forms.TextBox searchBox; // Already declared at the top
        // private System.Windows.Forms.Button searchButton; // Already declared at the top
        // private System.Windows.Forms.Panel panel1; // Renamed to panelTopBar, ensure old one is removed or new one takes its place
        // private System.Windows.Forms.Panel panelTopBar; // Now declared at the top
        // private System.Windows.Forms.Label labelGG; // Already declared at the top
        // private System.Windows.Forms.Label label1; // Already declared at the top
        // private System.Windows.Forms.Button ButtonClose; // Already declared at the top
        // private System.Windows.Forms.Button ButtonMaximaze; // Already declared at the top
        // private System.Windows.Forms.Button ButtonMinimaze; // Already declared at the top
    }
}