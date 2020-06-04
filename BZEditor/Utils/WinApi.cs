using System.Runtime.InteropServices;
using System.Text;

namespace BZEditor
{
    internal static class WinApi
    {
        [DllImport("Kernel32.dll")]
        public static extern bool WritePrivateProfileString(string section, string key, string value, string file);

        [DllImport("Kernel32.dll")]
        public static extern int GetPrivateProfileString(string section, string key, string defaultValue,
                                                         StringBuilder value, int maxValueLen, string file);
    }
}