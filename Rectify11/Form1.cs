using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rectify11.Backend;
using System.Threading;
using KPreisser.UI;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic;
namespace Rectify11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Opacity = 0;
            treeView1.AfterCheck += AfterChk;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            licPage.AllowNext = false;
            if (checkBox1.Checked) licPage.AllowNext = true;
        }

        private void CancelClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }
        private void pageChanged(object sender, EventArgs e)
        {

            if (wizardControl1.SelectedPage == FinishPage)
            {
                InstallSequence installSequence = new InstallSequence();

                installSequence.StartPatching(this, FileListBackend.FileListSelected(treeView1), 
                    FileListBackend.FileListFull(Variables.r11Files, Variables.sysDrive),
                    ExtrasBackend.ExtrasList(Variables.Extras));

                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 1000;
                timer.Enabled = true;
                timer.Tick += Timer_Tick;

            }
        }
        private int i = 20;
        private void Timer_Tick(object sender, EventArgs e)
        {
            label2.Text = "Restarting in " + i.ToString() + " seconds";
            if (i == 0) Interaction.Shell(Path.Combine(Variables.sys32, "shutdown.exe") + " /r /f /t 0", AppWinStyle.Hide, true);
            else i--;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            Form1Backend.ShowProgressDialog("Please wait", "Extracting Files...", "Installer is extracting files for installation.", this, Icon);
            Form1Backend.mainBackend.initProcedure();

            Thread.Sleep(2000);
            Form1Backend.ChangeDialogText("Please wait", "Preparing Files...", "Installer is reading the system files for preparation.", Icon);
            Form1Backend.PrepareTree(treeView1);

            Thread.Sleep(2000);
            Form1Backend.CloseProgressDialog(this);
        }
        private void FrmClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (Form1Backend.ShowCancelConf()) { e.Cancel = false; }
        }

        private void AfterChk(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode n in e.Node.Nodes)
            {
                n.Checked = e.Node.Checked;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Interaction.Shell(Path.Combine(Variables.sys32, "shutdown.exe") + " /r /f /t 0", AppWinStyle.Hide, true);
        }
    }
}
