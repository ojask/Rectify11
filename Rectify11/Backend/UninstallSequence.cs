using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Microsoft.Win32;

namespace Rectify11.Backend
{
    class UninstallSequence
    {
        public static void StartUninstalling(Form frm, List<string> FileListSelected, List<FileItem> FileListFull, List<Extra> ExtrasList)
        {
            UIBackend.ShowProgressDialog("Preparing Files...", "Installer is about to begin uninstallation...", "", frm, frm.Icon);
            Thread.Sleep(2000);
            var a = FileListFull;

            if (Directory.Exists(Variables.trash)) Directory.Delete(Variables.trash, true);
            Directory.CreateDirectory(Variables.trash);

            for (int i = 0; i < a.Count; i++)
            {
                if (FileListSelected.Contains(a[i].path))
                {
                    UIBackend.ChangeDialogText("Restoring Files...", "Please wait while the installer is restoring files.",
                    a[i].Name + " (" + ((i * 100) / a.Count).ToString() + "% complete)",
                    Properties.Resources.InstallingIcons);

                    if (!Directory.Exists(Path.GetDirectoryName(a[i].path).Replace(Variables.sysDrive, Variables.trash)))
                        Directory.CreateDirectory(Path.GetDirectoryName(a[i].path).Replace(Variables.sysDrive, Variables.trash));

                    UIBackend.mainBackend.SetPerms(Path.GetDirectoryName(a[i].path), "administrators", true);
                    UIBackend.mainBackend.SetPerms(a[i].path, "administrators", true);

                    try
                    {
                        NativeMethods.MoveFileEx(a[i].path, a[i].path.Replace(Variables.sysDrive, Variables.trash),
                        NativeMethods.MoveFileFlags.MOVEFILE_REPLACE_EXISTING);
                    }
                    catch { }

                    try
                    {
                        NativeMethods.MoveFileEx(a[i].path.Replace(Variables.sysDrive, Variables.Backup), a[i].path,
                        NativeMethods.MoveFileFlags.MOVEFILE_REPLACE_EXISTING);
                    }
                    catch { }

                    UIBackend.mainBackend.SetPerms(a[i].path, @"NT SERVICE\TrustedInstaller", false);
                    UIBackend.mainBackend.SetPerms(Path.GetDirectoryName(a[i].path), @"NT SERVICE\TrustedInstaller", false);
                    Thread.Sleep(200);
                }
            }
            ThemesBackend.Uninstall();
            for (int i = 0; i < ExtrasList.Count; i++)
            {
                if (FileListSelected.Contains(ExtrasList[i].Name))
                {
                    UIBackend.ChangeDialogText("Uninstalling Extras...", "Please wait while the installer is uninstalling extras.",
                    "Uninstalling Extras: " + ExtrasList[i].Name, ExtrasList[i].ExtraIcon);
                    try { ExtrasList[i].UninstallExtra(); } catch { }
                    Thread.Sleep(4000);
                }
            }
            UIBackend.ChangeDialogText("Cleaning up", "Finishing uninstallation, please wait...", "", Properties.Resources.done);
            RemoveFromControlPanel();
            PerformCleanup();
            UIBackend.CloseProgressDialog(frm);
        }
        private static void PerformCleanup()
        {
            DirectoryInfo directory = new DirectoryInfo(Path.Combine(Variables.UserDir, "appdata", "local", "microsoft", "windows", "explorer"));
            var a = directory.GetFiles("*.db", SearchOption.AllDirectories);
            for (int i = 0; i < a.Length; i++)
            {
                UIBackend.mainBackend.SetPerms(a[i].FullName, "administrators", true);
                UIBackend.mainBackend.SetPerms(a[i].DirectoryName, "administrators", true);
                try { NativeMethods.MoveFileEx(a[i].FullName, Path.Combine(Variables.trash, a[i].Name), NativeMethods.MoveFileFlags.MOVEFILE_REPLACE_EXISTING); }
                catch { }
            }
            UIBackend.mainBackend.KillExtrasIfRunning();
        }
        private static void RemoveFromControlPanel()
        {
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true);
            key.DeleteSubKey("Rectify11", false);
        }
    }
}
