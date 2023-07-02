using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Rectify11.Backend
{
    class FileListBackend
    {
        private static List<string> Tmp = new List<string>();
        public static List<FileItem> FileListFull(string Dir, string sysDrive)
        {
            DirectoryInfo directory = new DirectoryInfo(Dir);
            List<FileItem> icn = new List<FileItem>();
            var a = directory.GetFiles("*.*", SearchOption.AllDirectories);
            for (int i = 0; i < a.Count(); i++)
            {
                if (File.Exists(a[i].FullName.Replace(Dir, Variables.sysDrive).Replace(".res", "")))
                {
                    FileItem ico = new FileItem();
                    ico.Name = a[i].Name.Replace(".res", "");
                    ico.fileInfo = a[i];
                    ico.path = a[i].FullName.Replace(Dir, sysDrive).Replace(".res", "");
                    if (ico.Name.Contains(".mun")) ico.fileType = FileItem.FileType.basic;
                    else ico.fileType = FileItem.FileType.advanced;
                    icn.Add(ico);
                } 
            }
            return icn;
        }
        private static void lookupChk(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                { Tmp.Add(node.Text); }
                lookupChk(node.Nodes);
            }
        }
        public static List<string> FileListSelected(TreeView treeView)
        {
            lookupChk(treeView.Nodes);
            return Tmp;
        }
    }
    class FileItem
    {
        public string Name { get; set; }
        public string path { get; set; }
        public FileType fileType { get; set; }
        public FileInfo fileInfo { get; set; }
        public enum FileType { advanced, basic };
    }
}
