using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Rectify11
{
    class Variables
    {
        public static string sysDrive = Environment.GetEnvironmentVariable("systemdrive");
        public static string windir = Environment.GetEnvironmentVariable("windir");
        public static string sys32 = Path.Combine(windir, "System32");
        public static string sysRes =  Path.Combine(windir,"SystemResources");
        public static string r11Fldr = Path.Combine(windir, "Rectify11");
        public static string r11Files = Path.Combine(windir, "Rectify11","Files");
        public static string Backup = Path.Combine(windir, "Rectify11", "Backup");
        public static string tmp = Path.Combine(windir, "Rectify11", "Tmp");
        public static string trash = Path.Combine(windir, "Rectify11", "Trash");
        public static string UserDir = Environment.GetEnvironmentVariable("userprofile");
        public static string Extras = Path.Combine(r11Fldr, "Extras");
    }
}
