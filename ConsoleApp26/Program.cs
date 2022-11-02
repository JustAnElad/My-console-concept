using System;
using System.IO;
using System.Linq;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console Settings
            Console.SetWindowSize(90, 40); //Sets console size
            Console.Title = "My App"; // Sets console name
            ConsoleHelper.SetCurrentFont("consolas", 20); //Sets text size
            WindowUtility.MoveWindowToCenter();//Moves window to center

            //Paths
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //Desktop Path
            string AppPath = Path + "/My App";          // App Path
            string AccPath = AppPath + "/accounts";     // Accounts Directory
            string EmailsPath = AppPath + "/emails.txt";// Emails List
            string IdPath = AppPath + "/IDs.txt";       // Ids List
            string RemPath = AppPath + "/Rem.txt";      // Remember 
            //Variables
            string Text;
            string input = "default";                               //Input 1
            string input_2 = "default";                             // input 2
            string Tmp_str;                             // Temp String (used for short popurse)
            int ID;                                     // ID Variable
            int Permission;                             // Permission variable

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
            while (input != "/Exit" && input_2 != "/Exit" )
            {
            start_2:
                input = Console.ReadLine();
                switch (input)
                {
                    // sign in case
                    case "/Sign in":
                        Console.Clear();
                        //create Id
                        Random randomId = new Random();
                        ID = randomId.Next(100000, 999999);
                        string[] lines = File.ReadAllLines(IdPath);

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
                        input_2 = Console.ReadLine();
                        Tmp_str = File.ReadLines(EmailsPath).Skip(0).Take(1).First();
                        //check if it already exist
                        if (input_2.Equals(Tmp_str))
                        {
                            Console.Clear();
                            Console.WriteLine("this email is already exist");
                            goto email_signin;
                        }
                        //check if there is @ in the email
                        if (!input_2.Contains("@"))
                        {
                            Console.WriteLine("email does not exist, please check if you typed it correctly");
                            goto email_signin;
                        }
                        //separate the email suffix
                        int @Pos = input_2.IndexOf("@");
                        string emailSuffix = input_2.Substring(@Pos);
                        //check if the suffix is correct
                        if (!emailSuffix.Equals("@gmail.com"))
                        {
                            Console.WriteLine("email does not exist, please check if you typed it correctly");
                            goto email_signin;
                        }
                        string[] emails = File.ReadAllLines(EmailsPath);

                        foreach (string line in emails)
                        {

                            if (input_2.Equals(line))
                            {
                                Console.WriteLine("this email is already exist");
                                goto email_signin;
                            }

                        }
                    //password sign in
                    password_signin:
                        Console.Write("Please enter your password: ");
                        input = Console.ReadLine();
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
                        break;
                    case "Log in":
                        Console.Clear();

                    email_login:
                        Console.Write("please enter your email: ");
                        input = Console.ReadLine();
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
                        input_2 = Console.ReadLine();
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
                        break;

                    default:
                        Console.WriteLine("Invalid option");
                        goto start_2;
                        break;
                }
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
                input = Console.ReadLine();
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
                Console.ReadLine();

            
        }
    }
}
