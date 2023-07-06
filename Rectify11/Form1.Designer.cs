
namespace Rectify11
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Advanced");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Basic");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Icons", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Extras");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Themes");
            this.wizardControl1 = new AeroWizard.WizardControl();
            this.licPage = new AeroWizard.WizardPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.eulaImg = new System.Windows.Forms.PictureBox();
            this.selPage = new AeroWizard.WizardPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.selImg = new System.Windows.Forms.PictureBox();
            this.FinishPage = new AeroWizard.WizardPage();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.licPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eulaImg)).BeginInit();
            this.selPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selImg)).BeginInit();
            this.FinishPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.Add(this.licPage);
            this.wizardControl1.Pages.Add(this.selPage);
            this.wizardControl1.Pages.Add(this.FinishPage);
            this.wizardControl1.Size = new System.Drawing.Size(616, 437);
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Title = "Install Rectify11";
            this.wizardControl1.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wizardControl1.TitleIcon")));
            this.wizardControl1.Cancelling += new System.ComponentModel.CancelEventHandler(this.CancelClick);
            this.wizardControl1.SelectedPageChanged += new System.EventHandler(this.pageChanged);
            // 
            // licPage
            // 
            this.licPage.AllowBack = false;
            this.licPage.AllowNext = false;
            this.licPage.Controls.Add(this.checkBox1);
            this.licPage.Controls.Add(this.richTextBox1);
            this.licPage.Controls.Add(this.eulaImg);
            this.licPage.HelpText = "";
            this.licPage.Name = "licPage";
            this.licPage.Size = new System.Drawing.Size(569, 283);
            this.licPage.TabIndex = 0;
            this.licPage.Text = "License Agreement";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(159, 249);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(149, 19);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "I accept this agreement";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.richTextBox1.Location = new System.Drawing.Point(160, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(409, 268);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // eulaImg
            // 
            this.eulaImg.Image = global::Rectify11.Properties.Resources.eula;
            this.eulaImg.Location = new System.Drawing.Point(-3, 54);
            this.eulaImg.Name = "eulaImg";
            this.eulaImg.Size = new System.Drawing.Size(147, 147);
            this.eulaImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.eulaImg.TabIndex = 2;
            this.eulaImg.TabStop = false;
            // 
            // selPage
            // 
            this.selPage.Controls.Add(this.treeView1);
            this.selPage.Controls.Add(this.selImg);
            this.selPage.Name = "selPage";
            this.selPage.Size = new System.Drawing.Size(569, 283);
            this.selPage.TabIndex = 1;
            this.selPage.Text = "Select what should be installed";
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.treeView1.Indent = 19;
            this.treeView1.ItemHeight = 18;
            this.treeView1.Location = new System.Drawing.Point(181, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Checked = true;
            treeNode1.Name = "Node3";
            treeNode1.Text = "Advanced";
            treeNode2.Checked = true;
            treeNode2.Name = "Node4";
            treeNode2.Text = "Basic";
            treeNode3.Checked = true;
            treeNode3.Name = "Node0";
            treeNode3.Text = "Icons";
            treeNode4.Checked = true;
            treeNode4.Name = "Node1";
            treeNode4.Text = "Extras";
            treeNode5.Checked = true;
            treeNode5.Name = "Node2";
            treeNode5.Text = "Themes";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            this.treeView1.Size = new System.Drawing.Size(388, 283);
            this.treeView1.TabIndex = 1;
            // 
            // selImg
            // 
            this.selImg.Image = global::Rectify11.Properties.Resources.installconf;
            this.selImg.Location = new System.Drawing.Point(16, 53);
            this.selImg.Name = "selImg";
            this.selImg.Size = new System.Drawing.Size(147, 147);
            this.selImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.selImg.TabIndex = 2;
            this.selImg.TabStop = false;
            // 
            // FinishPage
            // 
            this.FinishPage.AllowBack = false;
            this.FinishPage.AllowCancel = false;
            this.FinishPage.Controls.Add(this.button1);
            this.FinishPage.Controls.Add(this.pictureBox2);
            this.FinishPage.Controls.Add(this.label2);
            this.FinishPage.Controls.Add(this.label1);
            this.FinishPage.Controls.Add(this.pictureBox1);
            this.FinishPage.Name = "FinishPage";
            this.FinishPage.ShowCancel = false;
            this.FinishPage.ShowNext = false;
            this.FinishPage.Size = new System.Drawing.Size(569, 283);
            this.FinishPage.TabIndex = 3;
            this.FinishPage.Text = "Restart required";
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(456, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "Restart now";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Rectify11.Properties.Resources.progress;
            this.pictureBox2.Location = new System.Drawing.Point(258, 7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(291, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Restarting in: 20 seconds";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(256, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 45);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rectify11 has finished patching your system. \r\nA restart is required in order to " +
    "apply the changes\r\nproperly. \r\n";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Rectify11.Properties.Resources.incomplete;
            this.pictureBox1.Location = new System.Drawing.Point(25, 56);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(147, 147);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(616, 437);
            this.ControlBox = false;
            this.Controls.Add(this.wizardControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.licPage.ResumeLayout(false);
            this.licPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eulaImg)).EndInit();
            this.selPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.selImg)).EndInit();
            this.FinishPage.ResumeLayout(false);
            this.FinishPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl1;
        private AeroWizard.WizardPage licPage;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox eulaImg;
        private AeroWizard.WizardPage selPage;
        private System.Windows.Forms.PictureBox selImg;
        private AeroWizard.WizardPage FinishPage;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

