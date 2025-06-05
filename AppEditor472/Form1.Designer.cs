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
            this.SuspendLayout();
            // 
            // AppNameTextBox
            // 
            this.AppNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AppNameTextBox.Location = new System.Drawing.Point(12, 29);
            this.AppNameTextBox.Name = "AppNameTextBox";
            this.AppNameTextBox.Size = new System.Drawing.Size(350, 20);
            this.AppNameTextBox.TabIndex = 0;
            // 
            // PathNameTextBox
            // 
            this.PathNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathNameTextBox.Location = new System.Drawing.Point(12, 70);
            this.PathNameTextBox.Name = "PathNameTextBox";
            this.PathNameTextBox.Size = new System.Drawing.Size(350, 20);
            this.PathNameTextBox.TabIndex = 1;
            // 
            // AppButtonAdd
            // 
            this.AppButtonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AppButtonAdd.Location = new System.Drawing.Point(369, 81);
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
            this.AppButtonSave.Location = new System.Drawing.Point(369, 54);
            this.AppButtonSave.Name = "AppButtonSave";
            this.AppButtonSave.Size = new System.Drawing.Size(75, 23);
            this.AppButtonSave.TabIndex = 3;
            this.AppButtonSave.Text = "Save App";
            this.AppButtonSave.UseVisualStyleBackColor = true;
            this.AppButtonSave.Click += new System.EventHandler(this.AppButtonSave_Click);
            // 
            // GroupComboBox
            // 
            this.GroupComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GroupComboBox.FormattingEnabled = true;
            this.GroupComboBox.Location = new System.Drawing.Point(12, 111);
            this.GroupComboBox.Name = "GroupComboBox";
            this.GroupComboBox.Size = new System.Drawing.Size(200, 21);
            this.GroupComboBox.TabIndex = 4;
            this.GroupComboBox.SelectedIndexChanged += new System.EventHandler(this.GroupComboBox_SelectedIndexChanged);
            // 
            // GroupTextBox
            // 
            this.GroupTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupTextBox.Location = new System.Drawing.Point(218, 111);
            this.GroupTextBox.Name = "GroupTextBox";
            this.GroupTextBox.Size = new System.Drawing.Size(144, 20);
            this.GroupTextBox.TabIndex = 5;
            // 
            // GroupButtonAdd
            // 
            this.GroupButtonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupButtonAdd.Location = new System.Drawing.Point(368, 110);
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
            this.GroupButtonDelete.Location = new System.Drawing.Point(368, 139);
            this.GroupButtonDelete.Name = "GroupButtonDelete";
            this.GroupButtonDelete.Size = new System.Drawing.Size(75, 23);
            this.GroupButtonDelete.TabIndex = 7;
            this.GroupButtonDelete.Text = "Delete Group";
            this.GroupButtonDelete.UseVisualStyleBackColor = true;
            this.GroupButtonDelete.Click += new System.EventHandler(this.GroupButtonDelete_Click);
            // 
            // AppListBox
            // 
            this.AppListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AppListBox.HideSelection = false;
            this.AppListBox.Location = new System.Drawing.Point(12, 172);
            this.AppListBox.Name = "AppListBox";
            this.AppListBox.Size = new System.Drawing.Size(431, 266);
            this.AppListBox.TabIndex = 8;
            this.AppListBox.UseCompatibleStateImageBehavior = false;
            this.AppListBox.View = System.Windows.Forms.View.List;
            this.AppListBox.SelectedIndexChanged += new System.EventHandler(this.AppListBox_SelectedIndexChanged);
            // 
            // labelAppName
            // 
            this.labelAppName.AutoSize = true;
            this.labelAppName.Location = new System.Drawing.Point(12, 13);
            this.labelAppName.Name = "labelAppName";
            this.labelAppName.Size = new System.Drawing.Size(93, 13);
            this.labelAppName.TabIndex = 9;
            this.labelAppName.Text = "Application Name:";
            // 
            // labelPathName
            // 
            this.labelPathName.AutoSize = true;
            this.labelPathName.Location = new System.Drawing.Point(12, 54);
            this.labelPathName.Name = "labelPathName";
            this.labelPathName.Size = new System.Drawing.Size(63, 13);
            this.labelPathName.TabIndex = 10;
            this.labelPathName.Text = "Path Name:";
            // 
            // labelGroup
            // 
            this.labelGroup.AutoSize = true;
            this.labelGroup.Location = new System.Drawing.Point(12, 95);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(39, 13);
            this.labelGroup.TabIndex = 11;
            this.labelGroup.Text = "Group:";
            // 
            // AppButtonDelete
            // 
            this.AppButtonDelete.Location = new System.Drawing.Point(369, 26);
            this.AppButtonDelete.Name = "AppButtonDelete";
            this.AppButtonDelete.Size = new System.Drawing.Size(75, 23);
            this.AppButtonDelete.TabIndex = 12;
            this.AppButtonDelete.Text = "Delete App";
            this.AppButtonDelete.UseVisualStyleBackColor = true;
            this.AppButtonDelete.Click += new System.EventHandler(this.AppButtonDelete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 450);
            this.Controls.Add(this.AppButtonDelete);
            this.Controls.Add(this.labelGroup);
            this.Controls.Add(this.labelPathName);
            this.Controls.Add(this.labelAppName);
            this.Controls.Add(this.AppListBox);
            this.Controls.Add(this.GroupButtonDelete);
            this.Controls.Add(this.GroupButtonAdd);
            this.Controls.Add(this.GroupTextBox);
            this.Controls.Add(this.GroupComboBox);
            this.Controls.Add(this.AppButtonSave);
            this.Controls.Add(this.AppButtonAdd);
            this.Controls.Add(this.PathNameTextBox);
            this.Controls.Add(this.AppNameTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "App Editor";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormAppEditor_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormAppEditor_DragEnter);
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
    }
}