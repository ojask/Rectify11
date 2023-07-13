using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using KPreisser.UI;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Collections;
using Rectify11.Properties;
using System.Globalization;
using System.Reflection;

namespace Rectify11.Backend
{
    class MainBackend
    {
        public void RunAsTrustedInstaller(string FileName, string args, bool wait, AppWinStyle appWinStyle)
        {
            Interaction.Shell(Path.Combine(Variables.r11Fldr, "aRun.bin")
                    + " /EXEFilename " + '"' + FileName + '"'
                    + " /CommandLine " + "\'" + args + "\'"
                    + " /WaitProcess 1 /RunAs 8 /Run", appWinStyle, wait);
        }
        public void SetPerms(string file, string GroupOrUser, bool set)
        {
            if (set)
            {
                Interaction.Shell(Path.Combine(Variables.sys32, "takeown.exe") +
                    " /a /f " + file, AppWinStyle.Hide, true);

                Interaction.Shell(Path.Combine(Variables.sys32, "icacls.exe") +
                    " " + file + " /grant " + '"' + GroupOrUser + '"' +
                    ":F", AppWinStyle.Hide, true);
            }
            else
            {
                Interaction.Shell(Path.Combine(Variables.sys32, "icacls.exe") +
                    " " + file + " /grant " + '"' + GroupOrUser + '"' +
                    ":F", AppWinStyle.Hide, true);

                Interaction.Shell(Path.Combine(Variables.sys32, "icacls.exe") +
                    " " + file + " /setowner " + '"' + GroupOrUser + '"', AppWinStyle.Hide, true);
            }
        }
        public void Patch(string path, string resDir, string tmpDir, string sysdrive)
        {
            Interaction.Shell(Path.Combine(Variables.r11Fldr, "ResourceHacker.exe") +
                " -open " + path.Replace(sysdrive, resDir) + ".res" +
                " -save " + path.Replace(sysdrive, resDir) + ".res" +
                " -action changelanguage(0)", AppWinStyle.Hide, true);

            Interaction.Shell(Path.Combine(Variables.r11Fldr, "ResourceHacker.exe") +
                " -open " + path.Replace(sysdrive, tmpDir) +
                " -save " + path.Replace(sysdrive, tmpDir) +
                " -action changelanguage(0)", AppWinStyle.Hide, true);

            Interaction.Shell(Path.Combine(Variables.r11Fldr, "ResourceHacker.exe") +
                " -open " + path.Replace(sysdrive, tmpDir) +
                " -save " + path.Replace(sysdrive, tmpDir) +
                " -action addoverwrite" +
                " -resource " + path.Replace(sysdrive, resDir) + ".res", AppWinStyle.Hide, true);
        }
        public void initProcedure()
        {
            if (!Directory.Exists(Variables.r11Fldr)) Directory.CreateDirectory(Variables.r11Fldr);

            var a = new DirectoryInfo(Variables.r11Fldr).GetFiles("*.*", SearchOption.TopDirectoryOnly);
            var b = new DirectoryInfo(Variables.r11Fldr).GetDirectories();

            for(int i=0; i<a.Length; i++)
            {
                if(!a[i].FullName.Contains(Variables.Backup)) File.Delete(a[i].FullName);
            }

            for(int i=0; i<b.Length; i++)
            {
                if(!b[i].FullName.Contains(Variables.Backup)) Directory.Delete(b[i].FullName, true);
            }

            var rList = Resources.ResourceManager
                       .GetResourceSet(CultureInfo.CurrentCulture, true, true)
                       .Cast<DictionaryEntry>()
                       .Where(x => x.Value.GetType() == typeof(byte[]))
                       .Select(x => new { Name = x.Key.ToString(), Val = x.Value })
                       .ToList();

            for (int i=0; i<rList.Count; i++)
            {
                File.WriteAllBytes(Path.Combine(Variables.r11Fldr, rList[i].Name.Replace("_","")+".exe"), (byte[])rList[i].Val);
            }

            for (int i = 0; i < Variables.R11FldrList().Count; i++)
            {
                if (!Directory.Exists(Variables.R11FldrList()[i]))
                {
                    Directory.CreateDirectory(Variables.R11FldrList()[i]);
                }
            }

        }
        public void ExtractFiles()
        {

            Interaction.Shell(Path.Combine(Variables.r11Fldr, "7za.exe") +
            " x -o" + Path.Combine(Variables.r11Fldr) +
            " " + Path.Combine(Variables.r11Fldr, "Files.exe"), AppWinStyle.Hide, true);

            Interaction.Shell(Path.Combine(Variables.r11Fldr, "7za.exe") +
            " x -o" + Path.Combine(Variables.r11Fldr) +
            " " + Path.Combine(Variables.r11Fldr, "Extras.exe"), AppWinStyle.Hide, true);

        }
        public void KillExtrasIfRunning()
        {
            if (Directory.Exists(Variables.Extras))
            {
                var a = new DirectoryInfo(Variables.Extras).GetFiles("*.*", SearchOption.AllDirectories);
                if (a.Length > 0) for (int i = 0; i < a.Length; i++)
                    {
                        Interaction.Shell(Path.Combine(Variables.sys32, "taskkill.exe") + " /f /im " + '"' + a[i].Name + '"', AppWinStyle.Hide, true);
                    }
                Directory.Delete(Variables.Extras, true);
            }
        }
    }
}
