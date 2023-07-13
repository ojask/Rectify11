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
            UIBackend.ShowProgressDialog("Preparing Files...", "Installer is about to begin patching...", "", frm, frm.Icon);
            Thread.Sleep(2000);
            var a = FileListFull;
            for (int i = 0; i < a.Count; i++)
            {
                if (FileListSelected.Contains(a[i].path))
                {
                    UIBackend.ChangeDialogText("Patching Files...", "Please wait while the installer is patching files.",
                    a[i].Name + " (" + ((i * 100) / a.Count).ToString() + "% complete)",
                    Properties.Resources.InstallingIcons);

                    if (!Directory.Exists(Path.GetDirectoryName(a[i].path).Replace(Variables.sysDrive, Variables.Backup)))
                        Directory.CreateDirectory(Path.GetDirectoryName(a[i].path).Replace(Variables.sysDrive, Variables.Backup));

                    if (!Directory.Exists(Path.GetDirectoryName(a[i].path).Replace(Variables.sysDrive, Variables.tmp)))
                        Directory.CreateDirectory(Path.GetDirectoryName(a[i].path).Replace(Variables.sysDrive, Variables.tmp));

                    if (!Directory.Exists(Path.GetDirectoryName(a[i].path).Replace(Variables.sysDrive, Variables.trash)))
                        Directory.CreateDirectory(Path.GetDirectoryName(a[i].path).Replace(Variables.sysDrive, Variables.trash));

                    UIBackend.mainBackend.SetPerms(Path.GetDirectoryName(a[i].path), "administrators", true);
                    UIBackend.mainBackend.SetPerms(a[i].path, "administrators", true);

                    try
                    {
                        if(!File.Exists(a[i].path.Replace(Variables.sysDrive, Variables.Backup)))
                            File.Copy(a[i].path, a[i].path.Replace(Variables.sysDrive, Variables.Backup), true);

                        File.Copy(a[i].path, a[i].path.Replace(Variables.sysDrive, Variables.tmp), true);

                        NativeMethods.MoveFileEx(a[i].path, a[i].path.Replace(Variables.sysDrive, Variables.trash), 
                        NativeMethods.MoveFileFlags.MOVEFILE_REPLACE_EXISTING);
                    }
                    catch { }

                    try
                    {
                        UIBackend.mainBackend.Patch(a[i].path, Variables.r11Files, Variables.tmp, Variables.sysDrive);
                    }
                    catch { }

                    try
                    {
                        NativeMethods.MoveFileEx(a[i].path.Replace(Variables.sysDrive, Variables.tmp), a[i].path,
                        NativeMethods.MoveFileFlags.MOVEFILE_REPLACE_EXISTING);
                    }
                    catch { }

                    UIBackend.mainBackend.SetPerms(a[i].path, @"NT SERVICE\TrustedInstaller", false);
                    UIBackend.mainBackend.SetPerms(Path.GetDirectoryName(a[i].path), @"NT SERVICE\TrustedInstaller", false);
                }
            }
            for (int i = 0; i < ExtrasList.Count; i++)
            {
                if (FileListSelected.Contains(ExtrasList[i].Name))
                {
                    UIBackend.ChangeDialogText("Installing Extras...", "Please wait while the installer is installing extras.",
                    "Installing Extras: " + ExtrasList[i].Name, ExtrasList[i].ExtraIcon);
                    try { ExtrasList[i].InstallExtra(); } catch { }
                    Thread.Sleep(4000);
                }
            }
            UIBackend.ChangeDialogText("Cleaning up", "Finishing installation, please wait...", "", Properties.Resources.done);
            PerformCleanup();
            AddToControlPanel();
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

            var b = new DirectoryInfo(Variables.r11Fldr).GetFiles("*.*", SearchOption.TopDirectoryOnly);
            for (int i=0; i<b.Length; i++)
            {
                File.Delete(b[i].FullName);
            }
        }
        private static void AddToControlPanel()
        {
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true);
            var r11key = key.CreateSubKey("Rectify11", true);
            if (r11key != null)
            {
                r11key.SetValue("DisplayName", "Rectify11", RegistryValueKind.String);
                r11key.SetValue("DisplayVersion", Assembly.GetEntryAssembly()?.GetName().Version.ToString() ?? string.Empty, RegistryValueKind.String);
                r11key.SetValue("DisplayIcon", Path.Combine(Variables.r11Fldr, "uninst000.exe"), RegistryValueKind.String);
                r11key.SetValue("InstallLocation", Variables.r11Fldr, RegistryValueKind.String);
                r11key.SetValue("UninstallString", Path.Combine(Variables.r11Fldr, "uninst000.exe"), RegistryValueKind.String);
                r11key.SetValue("ModifyPath", Path.Combine(Variables.r11Fldr, "uninst000.exe"), RegistryValueKind.String);
                r11key.SetValue("NoRepair", 1, RegistryValueKind.DWord);
                r11key.SetValue("VersionMajor", Assembly.GetEntryAssembly()?.GetName().Version.Major.ToString() ?? string.Empty, RegistryValueKind.String);
                r11key.SetValue("VersionMinor", Assembly.GetEntryAssembly()?.GetName().Version.Minor.ToString() ?? string.Empty, RegistryValueKind.String);
                r11key.SetValue("Build", Assembly.GetEntryAssembly()?.GetName().Version.Build.ToString() ?? string.Empty, RegistryValueKind.String);
                r11key.SetValue("Publisher", "The Rectify11 Team", RegistryValueKind.String);
                r11key.SetValue("URLInfoAbout", "https://rectify11.net/", RegistryValueKind.String);
                key.Close();
            }
            key.Close();

            File.Copy(Assembly.GetExecutingAssembly().Location, Path.Combine(Variables.r11Fldr,"uninst000.exe"), true);
            var refs = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).GetFiles("*.dll", SearchOption.TopDirectoryOnly);
            for(int i=0; i<refs.Length; i++)
            {
                File.Copy(refs[i].FullName, Path.Combine(Variables.r11Fldr, refs[i].Name));
            }

        }
    }
}
