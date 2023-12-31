﻿using System;
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

                InstallSequence.StartPatching(this, FileListBackend.FileListSelected(treeView1), 
                    FileListBackend.FileListFull(Variables.r11Files, Variables.sysDrive),
                    ExtrasBackend.ExtrasList(Variables.Extras), treeView1.Nodes[2].Checked);

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
            if (i == 0) UIBackend.mainBackend.EndProcedure();
            else i--;
        }
        private List<theme> thm { get; set; }
        private void Form1_Shown(object sender, EventArgs e)
        {
            UIBackend.ShowProgressDialog("Please wait", "Extracting Files...", "Installer is extracting files for installation.", this, Icon);
            UIBackend.mainBackend.KillExtrasIfRunning();
            UIBackend.mainBackend.initProcedure();
            UIBackend.mainBackend.ExtractFiles();

            Thread.Sleep(2000);
            UIBackend.ChangeDialogText("Please wait", "Preparing Files...", "Installer is reading the system files for preparation.", Icon);
            UIBackend.PrepareTree(treeView1, FileListBackend.FileListFull(Variables.r11Files, Variables.sysDrive), ExtrasBackend.ExtrasList(Variables.Extras));
            thm = ThemesBackend.themesList(Variables.r11themeDir);
            UIBackend.PrepareThemePage(comboBox1, pictureBox3, thm);

            Thread.Sleep(2000);
            UIBackend.CloseProgressDialog(this);
        }
        private void FrmClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (UIBackend.ShowCancelConf()) { e.Cancel = false; }
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
            UIBackend.mainBackend.EndProcedure();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox3.Image = ThemesBackend.themesList(Variables.r11themeDir)[comboBox1.SelectedIndex].preview;
        }
    }
}
