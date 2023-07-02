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

namespace Rectify11.Backend
{
    class MainBackend
    {
        public void RunAsTrustedInstaller(string FileName, string args, bool wait, AppWinStyle appWinStyle)
        {
            Interaction.Shell(Path.Combine(Variables.r11Fldr, "aRun.exe")
                    + " /EXEFilename " + '"' + FileName + '"'
                    + " /CommandLine " + "\'" + args + "\'"
                    + " /WaitProcess 1 /RunAs 8 /Run", appWinStyle, wait);
        }
        public void SetPerms(string file, string GroupOrUser)
        {
            Interaction.Shell(Path.Combine(Variables.sys32, "takeown.exe") +
                " /a /f " + file, AppWinStyle.Hide, true);

            Interaction.Shell(Path.Combine(Variables.sys32, "icacls.exe") +
                " " + file + " /grant " + GroupOrUser +
                ":F", AppWinStyle.Hide, true);
        }
        public void ResetPerms(string file)
        {
            Interaction.Shell(Path.Combine(Variables.sys32, "icacls.exe") +
                " " + file + " /grant " + '"' + @"NT SERVICE\TrustedInstaller" + '"' +
                ":F", AppWinStyle.Hide, true);

            Interaction.Shell(Path.Combine(Variables.sys32, "icacls.exe") +
                " " + file + " /setowner " + '"' + @"NT SERVICE\TrustedInstaller" + '"', AppWinStyle.Hide, true);
        }
        public void Patch(string path, string resDir, string tmpDir, string Backup, string trash, string sysdrive)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path).Replace(sysdrive, Backup)))
                Directory.CreateDirectory(Path.GetDirectoryName(path).Replace(sysdrive, Backup));

            if (!Directory.Exists(Path.GetDirectoryName(path).Replace(sysdrive, trash)))
                Directory.CreateDirectory(Path.GetDirectoryName(path).Replace(sysdrive, trash));

            if (!Directory.Exists(Path.GetDirectoryName(path).Replace(sysdrive, tmpDir)))
                Directory.CreateDirectory(Path.GetDirectoryName(path).Replace(sysdrive, tmpDir));

            if (!File.Exists(path.Replace(sysdrive, tmpDir)))
                File.Copy(path, path.Replace(sysdrive, tmpDir), true);

            if (!File.Exists(path.Replace(sysdrive, Backup)))
                File.Copy(path, path.Replace(sysdrive, Backup), true);

            Interaction.Shell(Path.Combine(Variables.r11Fldr, "rh.exe") +
                " -open " + path.Replace(sysdrive, resDir) + ".res" +
                " -save " + path.Replace(sysdrive, resDir) + ".res" +
                " -action changelanguage(0)", AppWinStyle.Hide, true);

            Interaction.Shell(Path.Combine(Variables.r11Fldr, "rh.exe") +
                " -open " + path.Replace(sysdrive, tmpDir) +
                " -save " + path.Replace(sysdrive, tmpDir) +
                " -action changelanguage(0)", AppWinStyle.Hide, true);

            Interaction.Shell(Path.Combine(Variables.r11Fldr, "rh.exe") +
                " -open " + path.Replace(sysdrive, tmpDir) +
                " -save " + path.Replace(sysdrive, tmpDir) +
                " -action addoverwrite" +
                " -resource " + path.Replace(sysdrive, resDir) + ".res", AppWinStyle.Hide, true);
        }
        public void initProcedure()
        {
            if (!Directory.Exists(Variables.r11Fldr)) Directory.CreateDirectory(Variables.r11Fldr);

            if (Directory.Exists(Variables.r11Files)) Directory.Delete(Variables.r11Files, true);
            if (!Directory.Exists(Variables.r11Files)) Directory.CreateDirectory(Variables.r11Files);

            if (Directory.Exists(Variables.tmp)) Directory.Delete(Variables.tmp, true);
            if (!Directory.Exists(Variables.tmp)) Directory.CreateDirectory(Variables.tmp);

            if (Directory.Exists(Variables.trash)) Directory.Delete(Variables.trash, true);
            if (!Directory.Exists(Variables.trash)) Directory.CreateDirectory(Variables.trash);

            if (!Directory.Exists(Variables.Backup)) Directory.CreateDirectory(Variables.Backup);

            if (!File.Exists(Path.Combine(Variables.r11Fldr, "7za.exe")))
                File.WriteAllBytes(Path.Combine(Variables.r11Fldr, "7za.exe"), Properties.Resources._7za);

            if (!File.Exists(Path.Combine(Variables.r11Fldr, "RH.exe")))
                File.WriteAllBytes(Path.Combine(Variables.r11Fldr, "RH.exe"), Properties.Resources.ResourceHacker);

            if (!File.Exists(Path.Combine(Variables.r11Fldr, "aRun.exe")))
                File.WriteAllBytes(Path.Combine(Variables.r11Fldr, "aRun.exe"), Properties.Resources.AdvancedRun);

            if (!File.Exists(Path.Combine(Variables.r11Fldr, "files.7z")))
                File.WriteAllBytes(Path.Combine(Variables.r11Fldr, "files.7z"), Properties.Resources.Files);

            if (!File.Exists(Path.Combine(Variables.r11Fldr, "extras.7z")))
                File.WriteAllBytes(Path.Combine(Variables.r11Fldr, "extras.7z"), Properties.Resources.Extras);

            Interaction.Shell(Path.Combine(Variables.r11Fldr, "7za.exe") +
                        " x -o" + Path.Combine(Variables.r11Fldr) +
                        " " + Path.Combine(Variables.r11Fldr, "Files.7z"), AppWinStyle.Hide, true);

            Interaction.Shell(Path.Combine(Variables.r11Fldr, "7za.exe") +
            " x -o" + Path.Combine(Variables.r11Fldr) +
            " " + Path.Combine(Variables.r11Fldr, "Extras.7z"), AppWinStyle.Hide, true);

            File.Delete(Path.Combine(Variables.r11Fldr, "files.7z"));
            File.Delete(Path.Combine(Variables.r11Fldr, "extras.7z"));

        }
    }
}
