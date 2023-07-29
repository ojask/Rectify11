using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using IniParser;
using IniParser.Model;
using Microsoft.VisualBasic;
namespace Rectify11.Backend
{
    class ExtrasBackend
    {
        public static List<Extra> ExtrasList(string extrasDir)
        {
            List<Extra> ls = new List<Extra>();
            if (Directory.Exists(extrasDir)) { 
            var a = new DirectoryInfo(extrasDir).GetDirectories();
                for (int i = 0; i < a.Length; i++)
                {
                    Extra extra = new Extra();
                    var parser = new FileIniDataParser();
                    IniData data = parser.ReadFile(Path.Combine(a[i].FullName, "config.ini"));

                    try { extra.Name = data["ConfigFile"]["FriendlyName"]; } catch { }
                    try { extra.ExtraIcon = Icon.ExtractAssociatedIcon(Path.Combine(a[i].FullName, data["ConfigFile"]["Icon"])); } catch { }
                    try { extra.installType = data["ConfigFile"]["InstallType"]; } catch { }
                    try { extra.dirName = a[i].FullName; } catch { }
                    try { extra.dirNameHalf = a[i].Name; } catch { }
                    try { extra.ExeName = data["ConfigFile"]["exeName"]; } catch { }
                    try { extra.args = data["ConfigFile"]["args"]; } catch { }
                    try { extra.fileForSched = Path.Combine(a[i].FullName, data["ConfigFile"]["fileForSched"]); } catch { }
                    try { extra.DestDir = data["ConfigFile"]["destDir"]; } catch { }
                    try { extra.copyNumber = data["ConfigFile"]["copyNumber"]; } catch { }
                    try { extra.Uninstargs = data["ConfigFile"]["Uninstargs"]; } catch { }
                    try { extra.UninstExe = data["ConfigFile"]["UninstExe"]; } catch { }
                    try
                    {
                        for (int j = 0; j < int.Parse(extra.copyNumber); j++)
                        {
                            extra.copyLst.Add(data["ConfigFile"]["file" + i.ToString()]);
                        }
                    }
                    catch { }
                    ls.Add(extra);
                }
            }
            return ls;
        }

    }
    class Extra
    {
        public string Name { get; set; }
        public string dirName { get; set; }
        public string dirNameHalf { get; set; }
        public string args { get; set; }
        public string Uninstargs { get; set; }
        public string UninstExe { get; set; }
        public string installType { get; set; }
        public string ExeName { get; set; }
        public string fileForSched { get; set; }
        public string DestDir { get; set; }
        public string copyNumber { get; set; }
        public List<string> copyLst { get; set; }
        public Icon ExtraIcon { get; set; }
        public void InstallExtra()
        {
            switch (installType.ToLower())
            {
                case "tasksched":
                    Interaction.Shell(Path.Combine(Variables.sys32, "schtasks.exe") + 
                        " /create /f /tn " + dirNameHalf + " /xml " + Path.Combine(dirName, fileForSched), AppWinStyle.Hide, true);
                    break;
                case "runexewithargs":
                    Interaction.Shell(ParsedPath(ExeName) + 
                        " " + ParsedPath(args), AppWinStyle.Hide, true);
                    break;
                case "copyall":
                    var b = ParsedPath(DestDir);
                    if (!Directory.Exists(b)) Directory.CreateDirectory(b);
                    var a = new DirectoryInfo(dirName).GetFiles("*.*", SearchOption.AllDirectories);
                    for(int i=0; i < a.Length; i++)
                    {
                        File.Copy(a[i].FullName, Path.Combine(b, a[i].Name), true);
                    }
                    break;
                case "copyspecific":
                    var dest = ParsedPath(DestDir);
                    var e = copyLst;
                    for(int i=1; i<=e.Count; i++)
                    {
                        File.Copy(Path.Combine(dirName, e[i]), Path.Combine(dest, e[i]), true);
                    }
                    break;
            }
        }
        public void UninstallExtra()
        {
            switch (installType.ToLower())
            {
                case "tasksched":
                    Interaction.Shell(Path.Combine(Variables.sys32, "schtasks.exe") + " /delete /f /tn "+dirNameHalf, AppWinStyle.Hide);
                    break;
                case "runexewithargs":
                    Interaction.Shell(ParsedPath(UninstExe) +
                        " " + ParsedPath(Uninstargs), AppWinStyle.Hide, true);
                    break;
                case "copyall":
                    var b = ParsedPath(DestDir);
                    if (!Directory.Exists(b)) Directory.CreateDirectory(b);
                    var a = new DirectoryInfo(dirName).GetFiles("*.*", SearchOption.AllDirectories);
                    for (int i = 0; i < a.Length; i++)
                    {
                        if(File.Exists(Path.Combine(b, a[i].Name))) File.Delete(Path.Combine(b, a[i].Name));
                    }
                    break;
                case "copyspecific":
                    var dest = ParsedPath(DestDir);
                    var e = copyLst;
                    for (int i = 1; i <= e.Count; i++)
                    {
                        if(File.Exists(Path.Combine(dest, e[i]))) File.Delete(Path.Combine(dest, e[i]));
                    }
                    break;
            }
        }
        private string ParsedPath(string s)
        {
            return s.Replace("%systemdrive%", Variables.sysDrive).Replace("Path(this)", dirName).Replace("%userprofile%", Variables.UserDir);
        }
    }
}
