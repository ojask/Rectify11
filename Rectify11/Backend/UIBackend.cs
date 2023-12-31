﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using KPreisser.UI;
using System.IO;
using System.Drawing;
using Microsoft.VisualBasic;

namespace Rectify11.Backend
{
    class UIBackend
    {
        private static TaskDialog td = new TaskDialog();
        public static MainBackend mainBackend = new MainBackend();
        public static void ChangeDialogText(string title, string instruction, string text, Icon icon)
        {
            td.Page.Text = text;
            td.Page.Instruction = instruction;
            td.Page.Title = title;
            td.Page.Icon = icon;
        }
        public static void CloseProgressDialog(Form frm1)
        {
            td.Close();
            frm1.Opacity = 1;
            frm1.Show();
        }
        public static bool ShowCancelConf()
        {
            DialogResult r = MessageBox.Show("Are you sure you want to quit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            bool a = false;
            if (r == DialogResult.Yes) a = true;
            return a;
        }
        public static void PrepareTree(TreeView treeView1, List<FileItem> files, List<Extra> Extras)
        {
            TreeStructure.PrepareRootTreeNodes(treeView1);

            var a = files;
            for (int i=0; i<a.Count; i++)
            {
                if (a[i].fileType == FileItem.FileType.advanced)
                {
                    TreeNode n = treeView1.Nodes[0].Nodes[0].Nodes.Add(a[i].Name);
                    n.Name = a[i].path;
                }
                else
                { 
                    TreeNode n = treeView1.Nodes[0].Nodes[1].Nodes.Add(a[i].Name);
                    n.Name = a[i].path;
                }
            }

            var b = Extras;
            for(int i=0; i<b.Count; i++)
            {
                TreeNode n = treeView1.Nodes[1].Nodes.Add(b[i].Name);
                n.Name = b[i].Name;
            }
        }
        public static void PrepareThemePage(ComboBox comboBox, PictureBox pictureBox, List<theme> lst) 
        {
            var a = lst;
            for(int i=0; i<a.Count; i++)
            {
                comboBox.Items.Add(a[i].Name);
            }
            comboBox.SelectedIndex = 0;
            pictureBox.Image = a[0].preview;
        }
        public static void ShowProgressDialog(string title, string instruction, string text, Form frm1, Icon icon)
        {
            frm1.Hide();
            td.Page.Icon = icon;
            td.Page.Instruction = instruction;
            td.Page.Title = title;
            td.Page.Text = text;
            td.Page.ProgressBar = new TaskDialogProgressBar();
            td.Page.ProgressBar.State = TaskDialogProgressBarState.Marquee;
            td.Page.StandardButtons = TaskDialogButtons.Help;
            Task.Run(() => td.Show());
        }
    }
}
