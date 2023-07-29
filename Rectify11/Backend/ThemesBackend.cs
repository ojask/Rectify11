using System.Collections.Generic;
using System.Drawing;
using System.IO;
using IniParser;
using IniParser.Model;
using Microsoft.VisualBasic;
using System.Diagnostics;
using Microsoft.Win32;
namespace Rectify11.Backend
{
    class ThemesBackend
    {
        public static void Install(string r11ThemeDir)
        {
            var a = new DirectoryInfo(r11ThemeDir).GetFiles("*.*", SearchOption.AllDirectories);
            for (int i = 0; i < a.Length; i++)
            {
                if (!Directory.Exists(Path.GetDirectoryName(a[i].FullName).Replace(r11ThemeDir, Variables.sysDrive)))
                    Directory.CreateDirectory(Path.GetDirectoryName(a[i].FullName).Replace(r11ThemeDir, Variables.sysDrive));
                if (!Directory.Exists(Path.GetDirectoryName(a[i].FullName).Replace(r11ThemeDir, Variables.trash)))
                    Directory.CreateDirectory(Path.GetDirectoryName(a[i].FullName).Replace(r11ThemeDir, Variables.trash));

                NativeMethods.MoveFileEx(a[i].FullName.Replace(r11ThemeDir, Variables.sysDrive), a[i].FullName.Replace(r11ThemeDir, Variables.trash),
                NativeMethods.MoveFileFlags.MOVEFILE_REPLACE_EXISTING);

                NativeMethods.MoveFileEx(a[i].FullName, a[i].FullName.Replace(r11ThemeDir, Variables.sysDrive),
                NativeMethods.MoveFileFlags.MOVEFILE_REPLACE_EXISTING);
            }
            string s = "x64";
            if (NativeMethods.IsArm64()) s = "arm64";
            Interaction.Shell(Path.Combine(Variables.windir, "SecureUxHelper-"+s+".exe")+" install", AppWinStyle.Hide, true);
        }
        public static void Uninstall()
        {
            Process.Start(Path.Combine(Variables.windir, "resources", "themes", "aero.theme"));
            if (!Directory.Exists(Variables.trash)) Directory.CreateDirectory(Variables.trash);
            if(Directory.Exists(Path.Combine(Variables.windir, "resources", "themes", "rectified")))
            {
                var a = new DirectoryInfo(Path.Combine(Variables.windir, "resources", "themes", "rectified")).GetFiles("*.*", SearchOption.AllDirectories);
                for(int i=0; i<a.Length; i++)
                {
                    NativeMethods.MoveFileEx(a[i].FullName, Path.Combine(Variables.trash, a[i].Name),
                    NativeMethods.MoveFileFlags.MOVEFILE_REPLACE_EXISTING);
                }
            }
        }
        public static List<theme> themesList(string r11ThemeDir)
        {
            List<theme> lst = new List<theme>();
            var a = new DirectoryInfo(Path.Combine(r11ThemeDir, "windows", "resources", "themes")).GetFiles("*.theme", SearchOption.TopDirectoryOnly);
            for(int i=0; i<a.Length; i++)
            {
                theme thm = new theme();
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(a[i].FullName);
                thm.Name = data["Theme"]["DisplayName"];
                thm.path = a[i].FullName.Replace(r11ThemeDir, Variables.sysDrive);
                try { thm.preview = new Bitmap(a[i].FullName.Replace(".theme", ".png")); } catch { }
                lst.Add(thm);
            }
            return lst;
        }
    }
    class theme
    {
        public string Name { get; set; }
        public string path { get; set; }
        public Bitmap preview { get; set; }
    }
}
