using System;
using System.IO;
using System.Windows.Forms;

namespace _7637_WS4
{
    public static class Utils
    {
        public static bool isFileExist(string filename)
        {
            if (!File.Exists(Application.StartupPath + "\\" + filename))
                return false;
            return true;
        }
    }
}
