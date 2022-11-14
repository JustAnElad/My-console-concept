using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    class SignUp
    {
        EnvSetUp EnvSetUp = new EnvSetUp();
        private IOUtil ioUtil;
       
        public SignUp()
        {
            ioUtil = new IOUtil();
        }

        public void start()
        {
            //Vars 
            string Tmp_str;
            string input;
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
            
            //create Id
            Random randomId = new Random();
            int ID = randomId.Next(100000, 999999);
            string[] lines = File.ReadAllLines(IdPath);
            

            foreach (string line in lines)
            {

                if (ID.Equals(Convert.ToInt32(line)))
                {
                    ID = randomId.Next(100000, 999999);
                }

            }

        //email sign up
        
            
            email = ioUtil.getEmailInputSignUp();
            
        //password sign up
        
            
            input = ioUtil.getPasswordInputSignUp();
            


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
                    Console.WriteLine("Unknown command");
                    goto remember;
                    break;
            }
            //Creates new user directory
            DirectoryInfo di = Directory.CreateDirectory(AccPath + "/" + email);
            di.Attributes = FileAttributes.Directory;

            //saves user's data (Email, Password and ID)
            File.AppendAllText(AccPath + "/" + email + "/Data.txt", email +
                                    Environment.NewLine + input + Environment.NewLine + ID);

            //saves the new email in the emails list
            File.AppendAllText(EmailsPath, email + Environment.NewLine);

            //saves the new ID in the Ids list
            File.AppendAllText(IdPath, ID + Environment.NewLine);

            //saves the remember choice
            File.WriteAllText(RemPath, Tmp_str);

            //Grants user permission
            Permission = ID;
            Console.WriteLine("your account has been created!");
        }
    }
}
