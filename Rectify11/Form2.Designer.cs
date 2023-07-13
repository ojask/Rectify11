
namespace Rectify11
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.wizardControl1 = new AeroWizard.WizardControl();
            this.uninstPage = new AeroWizard.WizardPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.finished = new AeroWizard.WizardPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.uninstPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.finished.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.Add(this.uninstPage);
            this.wizardControl1.Pages.Add(this.finished);
            this.wizardControl1.Size = new System.Drawing.Size(616, 437);
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Title = "Uninstall Rectify11";
            this.wizardControl1.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wizardControl1.TitleIcon")));
            this.wizardControl1.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControl1_Cancelling);
            this.wizardControl1.SelectedPageChanged += new System.EventHandler(this.wizardControl1_SelectedPageChanged);
            // 
            // uninstPage
            // 
            this.uninstPage.AllowBack = false;
            this.uninstPage.Controls.Add(this.treeView1);
            this.uninstPage.Controls.Add(this.pictureBox1);
            this.uninstPage.Name = "uninstPage";
            this.uninstPage.Size = new System.Drawing.Size(569, 283);
            this.uninstPage.TabIndex = 0;
            this.uninstPage.Text = "Select components to restore";
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.treeView1.Location = new System.Drawing.Point(176, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(393, 283);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Rectify11.Properties.Resources.installconf;
            this.pictureBox1.Location = new System.Drawing.Point(11, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(147, 147);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // finished
            // 
            this.finished.AllowBack = false;
            this.finished.AllowCancel = false;
            this.finished.AllowNext = false;
            this.finished.Controls.Add(this.button1);
            this.finished.Controls.Add(this.label1);
            this.finished.Controls.Add(this.pictureBox2);
            this.finished.Name = "finished";
            this.finished.ShowCancel = false;
            this.finished.ShowNext = false;
            this.finished.Size = new System.Drawing.Size(569, 283);
            this.finished.TabIndex = 1;
            this.finished.Text = "Uninstallation complete";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(471, 236);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "Restart now";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 60);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rectify11 has finished restoring your system. \r\nA restart is required in order to" +
    " apply the changes\r\nproperly. You can delete C:\\Windows\\Rectify11 folder\r\nmanual" +
    "ly after the restart.";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Rectify11.Properties.Resources.incomplete;
            this.pictureBox2.Location = new System.Drawing.Point(23, 56);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(147, 147);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(616, 437);
            this.ControlBox = false;
            this.Controls.Add(this.wizardControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Shown += new System.EventHandler(this.Form2_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.uninstPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.finished.ResumeLayout(false);
            this.finished.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl1;
        private AeroWizard.WizardPage uninstPage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TreeView treeView1;
        private AeroWizard.WizardPage finished;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}