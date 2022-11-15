using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    class EnvSetUp
    {
        public void setUpEnvaiarmentVaribules()
        {
            //Sets Paths
             string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //Desktop Path
             string AppPath = Path + "/My App";          // App Path
             string AccPath = AppPath + "/accounts";     // Accounts Directory
             string EmailsPath = AppPath + "/emails.txt";// Emails List
             string IdPath = AppPath + "/IDs.txt";       // Ids List
             string RemPath = AppPath + "/Rem.txt";      // Remember 
            Environment.SetEnvironmentVariable("Path",Path);
            Environment.SetEnvironmentVariable("AppPath", AppPath);
            Environment.SetEnvironmentVariable("AccPath",AccPath);
            Environment.SetEnvironmentVariable("EmailsPath", EmailsPath);
            Environment.SetEnvironmentVariable("IdPath", IdPath);
            Environment.SetEnvironmentVariable("RemPath", RemPath);

            //Sets Vars
            string Text = null;
            string input;                   //Input 1
            string input_2;                 // input 2
            string Tmp_str;                 // Temp String (used for short popurse)
            int Permission;                 // Permission variable    
            Environment.SetEnvironmentVariable("Text",Text);

        }

        public void setUpConsole()
        {
            Console.SetWindowSize(90, 40); //Sets console size
            Console.Title = "My App"; // Sets console name
            ConsoleHelper.SetCurrentFont("consolas", 20); //Sets text size
            WindowUtility.TryMoveWindowToCenter();//Moves window to center
        }
    }
}
