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

            var a = new DirectoryInfo(extrasDir).GetDirectories();
            for(int i=0; i < a.Length; i++)
            {
                Extra extra = new Extra();
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(Path.Combine(a[i].FullName, "config.ini"));

                extra.Name = data["ConfigFile"]["FriendlyName"];
                extra.ExtraIcon = Icon.ExtractAssociatedIcon(Path.Combine(a[i].FullName, data["ConfigFile"]["Icon"]));
                extra.installType = data["ConfigFile"]["InstallType"];
                extra.dirName = a[i].FullName;
                extra.dirNameHalf = a[i].Name;
                try { extra.ExeName = data["ConfigFile"]["exeName"]; } catch { }
                try { extra.args = data["ConfigFile"]["args"]; } catch { }
                try { extra.fileForSched = Path.Combine(a[i].FullName, data["ConfigFile"]["fileForSched"]); } catch { }
                try { extra.DestDir = data["ConfigFile"]["destDir"]; } catch { }
                try { extra.copyNumber = data["ConfigFile"]["copyNumber"]; } catch { }
                try
                {
                    for (int j = 0; j < Int32.Parse(extra.copyNumber); j++)
                    {
                        extra.copyLst.Add(data["ConfigFile"]["file" + i.ToString()]);
                    }
                }
                catch { }
                ls.Add(extra);
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
                        " /create /tn " + dirNameHalf + " /xml " + Path.Combine(dirName, fileForSched), AppWinStyle.Hide, true);
                    break;
                case "runexewithargs":
                    Interaction.Shell(ExeName.Replace("%systemdrive%", Variables.sysDrive).Replace("Path(this)", dirName).Replace("%userprofile%", Variables.UserDir) + 
                        " " + args.Replace("Path(this)", dirName).Replace("%systemdrive%", Variables.sysDrive).Replace("%userprofile%", Variables.UserDir), AppWinStyle.Hide, true);
                    break;
                case "copyall":
                    var b = DestDir.Replace("%systemdrive%", Variables.sysDrive).Replace("%userprofile%", Variables.UserDir).Replace("Path(this)", dirName);
                    if (!Directory.Exists(b)) Directory.CreateDirectory(b);
                    var a = new DirectoryInfo(dirName).GetFiles("*.*", SearchOption.AllDirectories);
                    for(int i=0; i < a.Length; i++)
                    {
                        File.Copy(a[i].FullName, Path.Combine(b, a[i].Name), true);
                    }
                    break;
                case "copyspecific":
                    var dest = DestDir.Replace("%systemdrive%", Variables.sysDrive).Replace("Path(this)", dirName).Replace("%userprofile%", Variables.UserDir);
                    var e = copyLst;
                    for(int i=1; i<=e.Count; i++)
                    {
                        File.Copy(Path.Combine(dirName, e[i]), Path.Combine(dest, e[i]), true);
                    }
                    break;
            }
        }
    }
}
