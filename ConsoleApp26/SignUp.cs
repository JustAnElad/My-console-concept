using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    class SignUp
    {
        private IOUtil ioUtil;
        //Gets Paths
        string AppPath = Environment.GetEnvironmentVariable("AppPath");         // App Path
        string AccPath = Environment.GetEnvironmentVariable("AccPath");      // Accounts Directory
        string EmailsPath = Environment.GetEnvironmentVariable("EmailsPath"); // Emails List
        string IdPath = Environment.GetEnvironmentVariable("IdPath");       // Ids List
        string RemPath = Environment.GetEnvironmentVariable("RemPath");      // Remember 

        //Vars 
        string Tmp_str;
        string input;
        string input_2;
        int Permission;

        public SignUp()
        {
            ioUtil = new IOUtil();
        }

        public void start(string path)
        {
            Console.Clear();
            //create Id
            Random randomId = new Random();
            int ID = randomId.Next(100000, 999999);
            string[] lines = File.ReadAllLines(path);
            string email;

            foreach (string line in lines)
            {

                if (ID.Equals(Convert.ToInt32(line)))
                {
                    ID = randomId.Next(100000, 999999);
                }

            }
        //email sign in
        email_signin:
            Console.Write("Please enter your email: ");
            email = ioUtil.getEmailInput();
            string[] emails = File.ReadAllLines(EmailsPath);
             
            foreach (string line in emails)
            {

                if (email.Equals(line))
                {
                    Console.WriteLine("this email is already exist");
                    goto email_signin;
                }

            }
        //password sign in
        password_signin:
            Console.Write("Please enter your password: ");
            input = ioUtil.getUserInput();
            //check password length
            if (input.Length < 8)
            {
                Console.WriteLine("password is too short, please use at least 8 chracters");
                goto password_signin;
            }
        password_signin_confirm:
            Console.Write("please confirm your password: ");
            if (Console.ReadLine() != input)
            {
                Console.WriteLine("password does not match, please check your password");
                goto password_signin_confirm;
            }


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
            DirectoryInfo di = Directory.CreateDirectory(AccPath + "/" + input_2);
            di.Attributes = FileAttributes.Directory;

            //saves user's data (Email, Password and ID)
            File.AppendAllText(AccPath + "/" + input_2 + "/Data.txt", input_2 +
                                    Environment.NewLine + input + Environment.NewLine + ID);

            //saves the new email in the emails list
            File.AppendAllText(EmailsPath, input_2 + Environment.NewLine);

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
