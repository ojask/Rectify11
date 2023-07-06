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
            Form1Backend.ShowProgressDialog("Please wait", "Preparing Files...", "Installer is reading the system files for preparation.", this, Icon);
            Form1Backend.PrepareTree(treeView1, FileListBackend.FileListFull(Variables.Backup, Variables.sysDrive), ExtrasBackend.ExtrasList(Variables.Extras));
            Thread.Sleep(2000);
            Form1Backend.CloseProgressDialog(this);
        }
        private void wizardControl1_Cancelling(object sender, CancelEventArgs e)
        {
            this.Close();
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (Form1Backend.ShowCancelConf()) { e.Cancel = false; }
        }
    }
}
