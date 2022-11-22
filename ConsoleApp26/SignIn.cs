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
            int Permission;
            //Paths
            string AccPath = Globals.AccPath;
            string RemPath = Globals.RemPath; 

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
            Permission = Convert.ToInt32(File.ReadLines(AccPath + "\\" + input + "/Data.txt").Skip(2).Take(1).First());
            File.WriteAllText(RemPath, Tmp_str + Environment.NewLine + Permission);
            Globals.Permission = Permission;
            Console.WriteLine("loged in successfully!");
        }
    }
}
