using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectify11.Backend
{
    class TreeStructure
    {
        public static string[] RootElements = { "Icons", "Extras","Themes"};
        public static string[] child_00_01 = { "Advanced", "Basic" };
        public static void PrepareRootTreeNodes(TreeView treeView1)
        {
            treeView1.Nodes.Clear();
            for (int i = 0; i < RootElements.Length; i++)
            {
                treeView1.Nodes.Add(RootElements[i]);
                if (i == 0)
                {
                    for (int j = 0; j < child_00_01.Length; j++)
                    {
                        treeView1.Nodes[i].Nodes.Add(child_00_01[j]);
                    }
                }
            }
        }
    }
}
