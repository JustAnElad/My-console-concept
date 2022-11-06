using ConsoleApp26;
using System;
using System.IO;
using System.Linq;


namespace ConsoleApp8
{
    class StartUp
    {

        static void Main(string[] args)
        {
            //Calls Objects
            SignUp signUp = new SignUp();
            EnvSetUp envSetUp = new EnvSetUp();
            IOUtil ioUtil = new IOUtil();

            //Console settings
            envSetUp.setUpEnvaiarmentVaribules();
            envSetUp.setUpConsole();

            //Gets Paths
            string Path = Environment.GetEnvironmentVariable("Path"); //Desktop path
            string AppPath = Environment.GetEnvironmentVariable("AppPath");         // App Path
            string AccPath = Environment.GetEnvironmentVariable("AccPath");      // Accounts Directory
            string EmailsPath = Environment.GetEnvironmentVariable("EmailsPath"); // Emails List
            string IdPath = Environment.GetEnvironmentVariable("IdPath");       // Ids List
            string RemPath = Environment.GetEnvironmentVariable("RemPath");      // Remember 

            //Variables
            string Text;
            string input;                   //Input 1
            string input_2;                 // input 2
            string Tmp_str;                 // Temp String (used for short popurse)
            int Permission;                 // Permission variable     
                               



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
            Text = "Just checking everything is in place... ";
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
            Start:
            Console.WriteLine("Hello Please choose one of the options below.");
            Console.WriteLine("/Sign in \n/Log in");
            
            start_2:
                input = ioUtil.getUserInput();
                switch (input)
                {
                    // sign in case
                    case "/Sign in":
                        signUp.start(IdPath);
                        goto main_menu;
                        break;
                    case "/Log in":
                        Console.Clear();

                    email_login:
                        Console.Write("please enter your email: ");
                        input = ioUtil.getUserInput();
                        Console.Clear();

                        // AccPath = DirPath + "/accounts";
                        //makes a list of all the directories inside "accounts" directory
                        string[] dirs = Directory.GetDirectories(AccPath);

                        foreach (var dir in dirs)
                        {
                            if ((AccPath + "\\" + input).Equals(dir))
                            {
                                goto password_login;
                            }
                        }
                        Console.WriteLine("this email does not exist");
                        goto email_login;

                    password_login:
                        Console.Write("please enter your password: ");
                        input_2 = ioUtil.getUserInput();
                        if (!input_2.Equals(File.ReadLines(AccPath + "\\" + input + "/Data.txt").Skip(1).Take(1).First()))
                        {
                            Console.WriteLine("password is incorrect, please check your password");
                            goto password_login;
                        }
                    remember_2:
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
                                goto remember_2;
                        }
                        File.WriteAllText(RemPath, Tmp_str);
                        Console.WriteLine("loged in successfully!");
                        goto main_menu;
                        break;

                    default:
                        Console.WriteLine("Invalid option");
                        goto start_2;
                        break;
                }
        
            main_menu:
                Console.WriteLine("hi");
                double input_4;
                string FilePath = Path + "/balance.txt";
                string hispath = Path + "/history.txt";
                if (!File.Exists(FilePath))
                {
                    File.WriteAllText(FilePath, "0");
                }
                if (!File.Exists(hispath))
                {
                    File.WriteAllText(hispath, "0");
                }
                double balance = Convert.ToDouble(File.ReadAllText(FilePath));
            while (true)
            {
                Console.WriteLine("1. Add\n 2. Substract\n 3. Show balance\n 4. History\n 5. Exit");
                input = ioUtil.getUserInput();
                while (input != "/Log out")
                {
                    switch (input)
                    {
                        //Add Amount
                        case "1":
                            Console.Clear();
                            Console.Write("Enter amount: ");
                            input_4 = double.Parse(Console.ReadLine());
                            balance = balance + input_4;
                            File.WriteAllText(FilePath, Convert.ToString(balance));
                            File.AppendAllText(hispath, Environment.NewLine + "+" + input_4);
                            break;
                        //Substract Amount
                        case "2":
                            Console.Clear();
                            Console.Write("Enter amount: ");
                            input_4 = double.Parse(Console.ReadLine());
                            balance = balance + input_4;
                            File.WriteAllText(FilePath, Convert.ToString(balance));
                            File.AppendAllText(hispath, Environment.NewLine + "-" + input_4);

                            break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine(balance);

                            break;
                        case "4":
                            Console.Clear();
                            Console.WriteLine(File.ReadAllText(hispath));
                            break;
                        case "5":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Unknown command");
                            break;
                    }
                }
                Permission = 0;
                File.WriteAllText(RemPath, "false");
                goto Start;
            }
                

            
        }
       
    }
}
