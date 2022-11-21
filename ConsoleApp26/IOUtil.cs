using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    class IOUtil
    {

        public string GetEmailInputSignIn()
        {
            string AccPath = Globals.AccPath;
            string input;

        start:
            Console.Write("please enter your email: ");
            input = GetUserInput();
            string[] dirs = Directory.GetDirectories(AccPath);

            foreach (var dir in dirs)
            {
                if ((AccPath + "\\" + input).Equals(dir))
                {
                    goto Continue;
                }
            }
            Console.WriteLine("this email does not exist");
            goto start;
        Continue:
            
            return input;
            

        }
        public string GetPasswordInputSignIn(string input)
        {
            string AccPath = Globals.AccPath;
            string input_2;
            start:

            Console.Write("please enter your password: ");
            input_2 = GetUserInput();
            if (!input_2.Equals(File.ReadLines(AccPath + "\\" + input + "/Data.txt").Skip(1).Take(1).First()))
            {
                Console.WriteLine("password is incorrect, please check your password");
                goto start;
            }
            return input_2;
        }

        public string GetEmailInputSignUp()
        {
            string EmailsPath = Globals.EmailsPath;
            string input;

        start:
            Console.Write("Please enter your email: ");
            input = GetUserInput();
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

        public string GetPasswordInputSignUp()
        {
            string input;
        start:
            Console.Write("Please enter your password: ");
            input = GetUserInput();
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
            if (GetUserInput() != input)
            {
                Console.WriteLine("password does not match, please check your password");
                goto password_signin_confirm;
            }
            return input;
        }

        public string GetUserInput()
        {
            string input = Console.ReadLine();
            if (input == "/Exit")
            {
                Environment.Exit(0);
            }
            else if(input == "/Sign out")
            {
                Globals.Permission = 0;
                StartUp.Menu();
            }
            return input;
        }
    }
}
