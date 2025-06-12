using System.Drawing;
using System.Windows.Forms;

namespace AppEditor472
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.AppNameTextBox = new System.Windows.Forms.TextBox();
            this.PathNameTextBox = new System.Windows.Forms.TextBox();
            this.AppButtonAdd = new System.Windows.Forms.Button();
            this.AppButtonSave = new System.Windows.Forms.Button();
            this.GroupComboBox = new System.Windows.Forms.ComboBox();
            this.GroupTextBox = new System.Windows.Forms.TextBox();
            this.GroupButtonAdd = new System.Windows.Forms.Button();
            this.GroupButtonDelete = new System.Windows.Forms.Button();
            this.AppListBox = new System.Windows.Forms.ListView();
            this.labelAppName = new System.Windows.Forms.Label();
            this.labelPathName = new System.Windows.Forms.Label();
            this.labelGroup = new System.Windows.Forms.Label();
            this.AppButtonDelete = new System.Windows.Forms.Button();
            this.groupBoxAppManagement = new System.Windows.Forms.GroupBox(); // Added GroupBox
            this.groupBoxGroupManagement = new System.Windows.Forms.GroupBox(); // Added GroupBox
            this.groupBoxAppManagement.SuspendLayout(); // Add to SuspendLayout/ResumeLayout
            this.groupBoxGroupManagement.SuspendLayout(); // Add to SuspendLayout/ResumeLayout
            this.SuspendLayout();
            //
            // groupBoxAppManagement
            //
            this.groupBoxAppManagement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAppManagement.Controls.Add(this.labelAppName);
            this.groupBoxAppManagement.Controls.Add(this.AppNameTextBox);
            this.groupBoxAppManagement.Controls.Add(this.labelPathName);
            this.groupBoxAppManagement.Controls.Add(this.PathNameTextBox);
            this.groupBoxAppManagement.Controls.Add(this.AppButtonDelete);
            this.groupBoxAppManagement.Controls.Add(this.AppButtonSave);
            this.groupBoxAppManagement.Controls.Add(this.AppButtonAdd);
            this.groupBoxAppManagement.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAppManagement.Name = "groupBoxAppManagement";
            this.groupBoxAppManagement.Size = new System.Drawing.Size(431, 130); // Adjusted size for content
            this.groupBoxAppManagement.TabIndex = 0;
            this.groupBoxAppManagement.TabStop = false;
            this.groupBoxAppManagement.Text = "Application Management";
            //
            // AppNameTextBox
            //
            this.AppNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AppNameTextBox.Location = new System.Drawing.Point(6, 38); // Relative to GroupBox
            this.AppNameTextBox.Name = "AppNameTextBox";
            this.AppNameTextBox.Size = new System.Drawing.Size(340, 20); // Adjusted size
            this.AppNameTextBox.TabIndex = 0;
            //
            // PathNameTextBox
            //
            this.PathNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathNameTextBox.Location = new System.Drawing.Point(6, 82); // Relative to GroupBox
            this.PathNameTextBox.Name = "PathNameTextBox";
            this.PathNameTextBox.Size = new System.Drawing.Size(340, 20); // Adjusted size
            this.PathNameTextBox.TabIndex = 1;
            //
            // AppButtonAdd
            //
            this.AppButtonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AppButtonAdd.Location = new System.Drawing.Point(350, 95); // Relative to GroupBox, aligned with PathNameTextBox
            this.AppButtonAdd.Name = "AppButtonAdd";
            this.AppButtonAdd.Size = new System.Drawing.Size(75, 23);
            this.AppButtonAdd.TabIndex = 2;
            this.AppButtonAdd.Text = "Browse App";
            this.AppButtonAdd.UseVisualStyleBackColor = true;
            this.AppButtonAdd.Click += new System.EventHandler(this.AppButtonAdd_Click);
            //
            // AppButtonSave
            //
            this.AppButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AppButtonSave.Location = new System.Drawing.Point(350, 68); // Relative to GroupBox
            this.AppButtonSave.Name = "AppButtonSave";
            this.AppButtonSave.Size = new System.Drawing.Size(75, 23);
            this.AppButtonSave.TabIndex = 3;
            this.AppButtonSave.Text = "Save App";
            this.AppButtonSave.UseVisualStyleBackColor = true;
            this.AppButtonSave.Click += new System.EventHandler(this.AppButtonSave_Click);
            //
            // AppButtonDelete
            //
            this.AppButtonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AppButtonDelete.Location = new System.Drawing.Point(350, 41); // Relative to GroupBox
            this.AppButtonDelete.Name = "AppButtonDelete";
            this.AppButtonDelete.Size = new System.Drawing.Size(75, 23);
            this.AppButtonDelete.TabIndex = 12;
            this.AppButtonDelete.Text = "Delete App";
            this.AppButtonDelete.UseVisualStyleBackColor = true;
            this.AppButtonDelete.Click += new System.EventHandler(this.AppButtonDelete_Click);
            //
            // labelAppName
            //
            this.labelAppName.AutoSize = true;
            this.labelAppName.Location = new System.Drawing.Point(6, 22); // Relative to GroupBox
            this.labelAppName.Name = "labelAppName";
            this.labelAppName.Size = new System.Drawing.Size(93, 13);
            this.labelAppName.TabIndex = 9;
            this.labelAppName.Text = "Application Name:";
            //
            // labelPathName
            //
            this.labelPathName.AutoSize = true;
            this.labelPathName.Location = new System.Drawing.Point(6, 66); // Relative to GroupBox
            this.labelPathName.Name = "labelPathName";
            this.labelPathName.Size = new System.Drawing.Size(63, 13);
            this.labelPathName.TabIndex = 10;
            this.labelPathName.Text = "Path Name:";
            //
            // groupBoxGroupManagement
            //
            this.groupBoxGroupManagement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGroupManagement.Controls.Add(this.labelGroup);
            this.groupBoxGroupManagement.Controls.Add(this.GroupComboBox);
            this.groupBoxGroupManagement.Controls.Add(this.GroupTextBox);
            this.groupBoxGroupManagement.Controls.Add(this.GroupButtonAdd);
            this.groupBoxGroupManagement.Controls.Add(this.GroupButtonDelete);
            this.groupBoxGroupManagement.Location = new System.Drawing.Point(12, 148); // Positioned below App Management
            this.groupBoxGroupManagement.Name = "groupBoxGroupManagement";
            this.groupBoxGroupManagement.Size = new System.Drawing.Size(431, 100); // Adjusted size
            this.groupBoxGroupManagement.TabIndex = 1;
            this.groupBoxGroupManagement.TabStop = false;
            this.groupBoxGroupManagement.Text = "Group Management";
            //
            // GroupComboBox
            //
            this.GroupComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GroupComboBox.FormattingEnabled = true;
            this.GroupComboBox.Location = new System.Drawing.Point(6, 38); // Relative to GroupBox
            this.GroupComboBox.Name = "GroupComboBox";
            this.GroupComboBox.Size = new System.Drawing.Size(200, 21);
            this.GroupComboBox.TabIndex = 4;
            this.GroupComboBox.SelectedIndexChanged += new System.EventHandler(this.GroupComboBox_SelectedIndexChanged);
            //
            // GroupTextBox
            //
            this.GroupTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupTextBox.Location = new System.Drawing.Point(212, 38); // Relative to GroupBox
            this.GroupTextBox.Name = "GroupTextBox";
            this.GroupTextBox.Size = new System.Drawing.Size(134, 20); // Adjusted size
            this.GroupTextBox.TabIndex = 5;
            //
            // GroupButtonAdd
            //
            this.GroupButtonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupButtonAdd.Location = new System.Drawing.Point(350, 37); // Relative to GroupBox
            this.GroupButtonAdd.Name = "GroupButtonAdd";
            this.GroupButtonAdd.Size = new System.Drawing.Size(75, 23);
            this.GroupButtonAdd.TabIndex = 6;
            this.GroupButtonAdd.Text = "Add Group";
            this.GroupButtonAdd.UseVisualStyleBackColor = true;
            this.GroupButtonAdd.Click += new System.EventHandler(this.GroupButtonAdd_Click);
            //
            // GroupButtonDelete
            //
            this.GroupButtonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupButtonDelete.Location = new System.Drawing.Point(350, 66); // Relative to GroupBox
            this.GroupButtonDelete.Name = "GroupButtonDelete";
            this.GroupButtonDelete.Size = new System.Drawing.Size(75, 23);
            this.GroupButtonDelete.TabIndex = 7;
            this.GroupButtonDelete.Text = "Delete Group";
            this.GroupButtonDelete.UseVisualStyleBackColor = true;
            this.GroupButtonDelete.Click += new System.EventHandler(this.GroupButtonDelete_Click);
            //
            // labelGroup
            //
            this.labelGroup.AutoSize = true;
            this.labelGroup.Location = new System.Drawing.Point(6, 22); // Relative to GroupBox
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(39, 13);
            this.labelGroup.TabIndex = 11;
            this.labelGroup.Text = "Group:";
            //
            // AppListBox
            //
            this.AppListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AppListBox.HideSelection = false;
            this.AppListBox.Location = new System.Drawing.Point(12, 258); // Positioned below Group Management
            this.AppListBox.Name = "AppListBox";
            this.AppListBox.Size = new System.Drawing.Size(431, 180); // Adjusted height
            this.AppListBox.TabIndex = 8;
            this.AppListBox.UseCompatibleStateImageBehavior = false;
            this.AppListBox.View = System.Windows.Forms.View.List;
            this.AppListBox.SelectedIndexChanged += new System.EventHandler(this.AppListBox_SelectedIndexChanged);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 450);
            this.Controls.Add(this.groupBoxGroupManagement); // Add GroupBox to form controls
            this.Controls.Add(this.groupBoxAppManagement); // Add GroupBox to form controls
            this.Controls.Add(this.AppListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "App Editor";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormAppEditor_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormAppEditor_DragEnter);
            this.groupBoxAppManagement.ResumeLayout(false); // Add to SuspendLayout/ResumeLayout
            this.groupBoxAppManagement.PerformLayout(); // Add to SuspendLayout/ResumeLayout
            this.groupBoxGroupManagement.ResumeLayout(false); // Add to SuspendLayout/ResumeLayout
            this.groupBoxGroupManagement.PerformLayout(); // Add to SuspendLayout/ResumeLayout
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AppNameTextBox;
        private System.Windows.Forms.TextBox PathNameTextBox;
        private System.Windows.Forms.Button AppButtonAdd;
        private System.Windows.Forms.Button AppButtonSave;
        private System.Windows.Forms.ComboBox GroupComboBox;
        private System.Windows.Forms.TextBox GroupTextBox;
        private System.Windows.Forms.Button GroupButtonAdd;
        private System.Windows.Forms.Button GroupButtonDelete;
        private System.Windows.Forms.ListView AppListBox;
        private System.Windows.Forms.Label labelAppName;
        private System.Windows.Forms.Label labelPathName;
        private System.Windows.Forms.Label labelGroup;
        private Button AppButtonDelete;
        private GroupBox groupBoxAppManagement; // Declare GroupBox
        private GroupBox groupBoxGroupManagement; // Declare GroupBox
    }
}