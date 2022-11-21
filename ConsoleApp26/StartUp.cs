using ConsoleApp26;
using System;
using System.IO;
using System.Linq;


namespace ConsoleApp26
{
    class StartUp
    {

        static void Main(string[] args)
        {
            //Calls Objects
            EnvSetUp envSetUp = new EnvSetUp();
           

            //Console settings
            envSetUp.SetupEnvironmentVariables();
            envSetUp.SetupConsole();

            //Gets Paths
            string AppPath = Globals.AppPath;        // App Path
            string AccPath = Globals.AccPath;    // Accounts Directory
            string EmailsPath = Globals.EmailsPath; // Emails List
            string IdPath = Globals.IdPath;       // Ids List
            string RemPath = Globals.RemPath;      // Remember 

            //Variables
            string input;                   //Input 1                // input 2
            string Tmp_str;                 // Temp String (used for short popurse) 




            //Checks If App Directory Exist
            if (!Directory.Exists(AppPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(AppPath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            //Checks If "Accounts" Directory Exist
            if (!Directory.Exists(AccPath))
            {
                Directory.CreateDirectory(AccPath);
            }
            //check if the file exist(ids, accounts, emails)
            if (!File.Exists(EmailsPath))
            {
                File.AppendAllText(EmailsPath, "default" + Environment.NewLine);
            }
            if (!File.Exists(IdPath))
            {
                File.Create(IdPath).Close();
            }
            if (!File.Exists(RemPath))
            {
                File.AppendAllText(RemPath, "false");
            }

            //Nice progress bar
            string Text = "Just checking everything is in place... ";
            Console.SetCursorPosition((Console.WindowWidth - Text.Length) / 2, Console.CursorTop);
            Console.WriteLine(Text);
            using (var progress = new ProgressBar())
            {
                for (int i = 0; i <= 100; i++)
                {
                    progress.Report((double)i / 100);
                    Thread.Sleep(20);
                }
            }
            Console.Clear();

            //Check if remember me
            Tmp_str = File.ReadAllText(RemPath);
            if (Tmp_str == "true")
            {
                goto main_menu;
            }
       
        main_menu:
            Menu();
            Console.ReadLine();
        }

        public static void Menu()
        {
            //Calls Objects
            IOUtil ioUtil = new IOUtil();
            SignIn signIn = new SignIn();
            SignUp signUp = new SignUp();
            //Variables
            string input;

            Console.WriteLine("Hello Please choose one of the options below.");
            Console.WriteLine("/Sign up \n/Sign in");

            do
            {
                input = ioUtil.GetUserInput();
                switch (input)
                {
                    // sign in case
                    case "/Sign up":
                        Console.Clear();
                        signUp.start();

                        break;
                    case "/Sign in":
                        Console.Clear();
                        signIn.start();

                        break;

                    default:
                        Console.WriteLine("Invalid option, please type again");
                        break;
                }
            }
            while (input != "/Sign up" && input != "Sign in");
            
            Console.WriteLine("hi");
            Console.ReadLine();

        }
        

    }

}

