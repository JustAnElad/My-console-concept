using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    public static class Globals
    {
        //Paths
        public static string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //Desktop Path
        public static string AppPath = Path + "/My App";
        public static string AccPath = AppPath + "/accounts";
        public static string EmailsPath = AppPath + "/emails.txt";// Emails List
        public static string IdPath = AppPath + "/IDs.txt";       // Ids List
        public static string RemPath = AppPath + "/Rem.txt";      // Remember 

        //Vars
        public static int Permission;
    }
}
