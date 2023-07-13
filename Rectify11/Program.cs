using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rectify11.Backend;
using System.Reflection;
using System.IO;
namespace Rectify11
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location).ToLower()=="uninst000") Application.Run(new Form2());
            else Application.Run(new Form1());
        }
    }
}
