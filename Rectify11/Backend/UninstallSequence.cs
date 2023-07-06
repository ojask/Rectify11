using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Rectify11.Backend
{
    class UninstallSequence
    {
        public static void StartUninstalling(Form frm, List<string> FileListSelected, List<FileItem> FileListFull, List<Extra> ExtrasList)
        {
            Form1Backend.ShowProgressDialog("Preparing Files...", "Installer is about to begin uninstallation...", "", frm, frm.Icon);
            Thread.Sleep(2000);
            var a = FileListFull;
            for (int i = 0; i < a.Count; i++)
            {
                if (FileListSelected.Contains(a[i].Name))
                {
                    Form1Backend.ChangeDialogText("Patching Files...", "Please wait while the installer is restoring files.",
                    a[i].Name + " (" + ((i * 100) / a.Count).ToString() + "% complete)",
                    Properties.Resources.InstallingIcons);

                    if (Directory.Exists(Variables.trash)) Directory.CreateDirectory(Variables.trash);

                    if (!Directory.Exists(Path.GetDirectoryName(a[i].path).Replace(Variables.sysDrive, Variables.trash)))
                        Directory.CreateDirectory(Path.GetDirectoryName(a[i].path).Replace(Variables.sysDrive, Variables.trash));

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
                        NativeMethods.MoveFileEx(a[i].path.Replace(Variables.sysDrive, Variables.Backup), a[i].path,
                        NativeMethods.MoveFileFlags.MOVEFILE_REPLACE_EXISTING);
                    }
                    catch { }

                    Form1Backend.mainBackend.SetPerms(a[i].path, @"NT SERVICE\TrustedInstaller", false);
                    Form1Backend.mainBackend.SetPerms(Path.GetDirectoryName(a[i].path), @"NT SERVICE\TrustedInstaller", false);
                    Thread.Sleep(500);
                }
            }
            for (int i = 0; i < ExtrasList.Count; i++)
            {
                if (FileListSelected.Contains(ExtrasList[i].Name))
                {
                    Form1Backend.ChangeDialogText("Installing Extras...", "Please wait while the installer is uninstalling extras.",
                    "Uninstalling Extras: " + ExtrasList[i].Name, ExtrasList[i].ExtraIcon);
                    try { ExtrasList[i].UninstallExtra(); } catch { }
                    Thread.Sleep(4000);
                }
            }
            Form1Backend.ChangeDialogText("Cleaning up", "Finishing uninstallation, please wait...", "", Properties.Resources.done);
            Form1Backend.CloseProgressDialog(frm);
        }
    }
}
