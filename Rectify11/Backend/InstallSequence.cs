using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using System.Reflection;

namespace Rectify11.Backend
{
    class InstallSequence
    {
        public static void StartPatching(Form frm, List<string> FileListSelected, List<FileItem> FileListFull, List<Extra> ExtrasList)
        {
            Form1Backend.ShowProgressDialog("Preparing Files...", "Installer is about to begin patching...", "", frm, frm.Icon);
            Thread.Sleep(2000);
            var a = FileListFull;
            for (int i = 0; i < a.Count; i++)
            {
                if (FileListSelected.Contains(a[i].Name))
                {
                    Form1Backend.ChangeDialogText("Patching Files...", "Please wait while the installer is patching files.",
                    a[i].Name + " (" + ((i * 100) / a.Count).ToString() + "% complete)",
                    Properties.Resources.InstallingIcons);

                    try
                    {
                        Form1Backend.mainBackend.Patch(a[i].path, Variables.r11Files, Variables.tmp, Variables.Backup, Variables.trash, Variables.sysDrive);
                    }
                    catch { }

                    Form1Backend.mainBackend.SetPerms(Path.GetDirectoryName(a[i].path), "administrators", false);
                    Form1Backend.mainBackend.SetPerms(a[i].path, "administrators", true);

                    try
                    {
                        NativeMethods.MoveFileEx(a[i].path, a[i].path.Replace(Variables.sysDrive, Variables.trash), 
                        NativeMethods.MoveFileFlags.MOVEFILE_REPLACE_EXISTING);
                    }
                    catch { }

                    try
                    {
                        NativeMethods.MoveFileEx(a[i].path.Replace(Variables.sysDrive, Variables.tmp), a[i].path,
                        NativeMethods.MoveFileFlags.MOVEFILE_REPLACE_EXISTING);
                    }
                    catch { }

                    Form1Backend.mainBackend.SetPerms(a[i].path, @"NT SERVICE\TrustedInstaller", false);
                    Form1Backend.mainBackend.SetPerms(Path.GetDirectoryName(a[i].path), @"NT SERVICE\TrustedInstaller", false);
                }
            }
            for (int i = 0; i < ExtrasList.Count; i++)
            {
                if (FileListSelected.Contains(ExtrasList[i].Name))
                {
                    Form1Backend.ChangeDialogText("Installing Extras...", "Please wait while the installer is installing extras.",
                    "Installing Extras: " + ExtrasList[i].Name, ExtrasList[i].ExtraIcon);
                    try { ExtrasList[i].InstallExtra(); } catch { }
                    Thread.Sleep(4000);
                }
            }
            Form1Backend.ChangeDialogText("Cleaning up", "Finishing installation, please wait...", "", Properties.Resources.done);
            PerformCleanup();
            Form1Backend.CloseProgressDialog(frm);
        }
        private static void PerformCleanup()
        {
            DirectoryInfo directory = new DirectoryInfo(Path.Combine(Variables.UserDir, "appdata", "local", "microsoft", "windows", "explorer"));
            var a = directory.GetFiles("*.db", SearchOption.AllDirectories);
            for (int i = 0; i < a.Length; i++)
            {
                Form1Backend.mainBackend.SetPerms(a[i].FullName, "administrators", true);
                Form1Backend.mainBackend.SetPerms(a[i].DirectoryName, "administrators", true);
                try { NativeMethods.MoveFileEx(a[i].FullName, Path.Combine(Variables.trash, a[i].Name), NativeMethods.MoveFileFlags.MOVEFILE_REPLACE_EXISTING); }
                catch { }
            }

            var b = new DirectoryInfo(Variables.r11Fldr).GetFiles("*.bin", SearchOption.TopDirectoryOnly);
            for (int i=0; i<b.Length; i++)
            {
                File.Delete(b[i].FullName);
            }
        }
    }
}
