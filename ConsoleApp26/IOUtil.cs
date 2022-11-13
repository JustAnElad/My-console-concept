using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    class IOUtil
    {


        public string getEmailInput()
        {
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //Desktop Path
            string AppPath = Path + "/My App";          // App Path
            string AccPath = AppPath + "/accounts";     // Accounts Directory
            string EmailsPath = AppPath + "/emails.txt";// Emails List
            string IdPath = AppPath + "/IDs.txt";       // Ids List
            string RemPath = AppPath + "/Rem.txt";      // Remember 
            string input;

        start:
            Console.Write("Please enter your email: ");
            input = getUserInput();
            if(input.Length >= 254)
            {
                Console.WriteLine("Email is too long, please use at most 254 characters");
                goto start;
            }
            string[] emails = File.ReadAllLines(EmailsPath);

            foreach (string line in emails)
            {

                if (input.Equals(line))
                {
                    Console.WriteLine("this email is already exist");
                    goto start;
                }

            }
            //check if there is @ in the email
            if (!input.Contains("@"))
            {
                Console.WriteLine("email does not exist, please check if you typed it correctly");
                goto start;
            }
            //separate the email suffix
            int @Pos = input.IndexOf("@");
            string emailSuffix = input.Substring(@Pos);
            //check if the suffix is correct
            if (!emailSuffix.Equals("@gmail.com"))
            {
                Console.WriteLine("email does not exist, please check if you typed it correctly");
                goto start;
            }


            return input;
        }

        public string getPasswordInputSignUp()
        {
            string input;
        start:
            Console.Write("Please enter your password: ");
            input = getUserInput();
            //check password length
            if (input.Length < 8)
            {
                Console.WriteLine("password is too short, please use at least 8 chracters");
                goto start;
            }
            else if (input.Length > 24)
            {
                Console.WriteLine("Password is too long, Please use at most 24 characters");
                goto start;
            }
        password_signin_confirm:
            Console.Write("please confirm your password: ");
            if (getUserInput() != input)
            {
                Console.WriteLine("password does not match, please check your password");
                goto password_signin_confirm;
            }
            return input;
        }

        public string getUserInput()
        {
            string input = Console.ReadLine();
            if (input == "/Exit")
            {
                Environment.Exit(0);
            }
            return input;
        }
    }
}
