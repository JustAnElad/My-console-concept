using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    class SignIn
    {
        private IOUtil ioUtil;

        public SignIn()
        {
            ioUtil = new IOUtil();
        }

        public void start()
        {
            //Vars 
            string Tmp_str;
            string input;
            string input_2;
            string email;
            int Permission;
            //Paths
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //Desktop Path
            string AppPath = Path + "/My App";          // App Path
            string AccPath = AppPath + "/accounts";     // Accounts Directory
            string EmailsPath = AppPath + "/emails.txt";// Emails List
            string IdPath = AppPath + "/IDs.txt";       // Ids List
            string RemPath = AppPath + "/Rem.txt";      // Remember 

            Console.Clear();

            //email
            input = ioUtil.GetEmailInputSignIn();

            //password
            input_2 = ioUtil.GetPasswordInputSignIn(input);

        remember:
            Console.WriteLine("remember me Yes / No.");
            switch (Console.ReadLine())
            {
                case "Yes":
                    Tmp_str = "true";
                    break;
                case "No":
                    Tmp_str = "false";
                    break;
                default:
                    goto remember;
            }
            File.WriteAllText(RemPath, Tmp_str);
            Permission = Convert.ToInt32(File.ReadLines(AccPath + "\\" + input + "/Data.txt").Skip(1).Take(1).First());
            Globals.Permission = Permission;
            Console.WriteLine("loged in successfully!");
        }
    }
}
