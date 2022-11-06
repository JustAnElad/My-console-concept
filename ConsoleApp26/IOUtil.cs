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
            string input;
            bool IsCorrectInput;
            do
            {    
                IsCorrectInput = true;
                input = getUserInput();
                //check if there is @ in the email
                if (!input.Contains("@"))
                {
                    IsCorrectInput = false;
                    Console.WriteLine("email does not exist, please check if you typed it correctly");
                }
                //separate the email suffix
                int @Pos = input.IndexOf("@");
                string emailSuffix = input.Substring(@Pos);
                //check if the suffix is correct
                if (!emailSuffix.Equals("@gmail.com"))
                {
                    IsCorrectInput = false;
                    Console.WriteLine("email does not exist, please check if you typed it correctly");
                }
            }
            while (!IsCorrectInput);

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
