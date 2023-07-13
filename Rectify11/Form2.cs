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
using Microsoft.VisualBasic;
using System.IO;

namespace Rectify11
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Shown(object sender, EventArgs e)
        {
            UIBackend.ShowProgressDialog("Please wait", "Preparing Files...", "Installer is reading the system files for preparation.", this, Icon);
            UIBackend.PrepareTree(treeView1, FileListBackend.FileListFull(Variables.Backup, Variables.sysDrive), ExtrasBackend.ExtrasList(Variables.Extras));
            Thread.Sleep(2000);
            UIBackend.CloseProgressDialog(this);
        }
        private void wizardControl1_Cancelling(object sender, CancelEventArgs e)
        {
            this.Close();
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (UIBackend.ShowCancelConf()) { e.Cancel = false; }
        }
        private void wizardControl1_SelectedPageChanged(object sender, EventArgs e)
        {
            if (wizardControl1.SelectedPage == finished)
            {
                UninstallSequence.StartUninstalling(this, FileListBackend.FileListSelected(treeView1), 
                    FileListBackend.FileListFull(Variables.Backup, Variables.sysDrive), ExtrasBackend.ExtrasList(Variables.Extras));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Interaction.Shell(Path.Combine(Variables.sys32, "shutdown.exe") + " /r /f /t 0", AppWinStyle.Hide, true);
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode n in e.Node.Nodes)
            {
                n.Checked = e.Node.Checked;
            }
        }
    }
}
