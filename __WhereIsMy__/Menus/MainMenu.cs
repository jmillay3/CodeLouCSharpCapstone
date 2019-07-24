using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace __WhereIsMy__.App.Menus
{

    public class MainMenu
    {
        public static string _dataFile;
        public static Delta.StuffDelta _stuffDelta;

        public static List<Stuffs.Stuff> _stuffList { get; set; }

        public static int DisplayMenu()
        {
            var response = 0;

            //get the DB directory and json file
            _dataFile = $@"{Program.JsonDataDirectory}Data\";
            _stuffDelta = new Delta.StuffDelta(_dataFile);

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" Where Is My Stuff? Useful Console App");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" 1. WHERE IS MY STUFF?!");
            Console.WriteLine(" 2. Manage My Stuff");
            Console.WriteLine(" 3. LEAVE ME ALONE! (...exit...)");
            Console.WriteLine();
            Console.Write("What's it gonna be King Fartsniffer? ");

            //snag the response
            response = Convert.ToInt32(Console.ReadLine());
            return response;
        }
        public static void Run()
        {
            int userInput = 0;
            do
            {
                try
                {
                    //get the selection
                    userInput = DisplayMenu();

                    switch (userInput)
                    {
                        case 1:
                            ManageMenu.GetAll();
                            break;
                        case 2:
                            ManageMenu.Run();
                            break;
                        case 3:
                            Console.WriteLine();
                            Console.WriteLine("Wonderful, I'm tired of dealing with you anyways...Goodbye");
                            System.Threading.Thread.Sleep(1000);
                            Console.WriteLine("...Adios...");
                            System.Threading.Thread.Sleep(1000);
                            Console.WriteLine("...Auf Wiedersehen...");
                            System.Threading.Thread.Sleep(1000);
                            Console.WriteLine("Did you know I can speak over 50 languages?");
                            System.Threading.Thread.Sleep(1000);
                            Console.WriteLine("...Au Revoir...");
                            System.Threading.Thread.Sleep(1000);
                            Console.WriteLine("...SAYONARA MOTHA___...");
                            System.Threading.Thread.Sleep(1000);
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine(" That's NOT an option...Come on now...");
                            System.Threading.Thread.Sleep(2000);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(" Unexpected Error:");
                    Console.WriteLine(e);
                    System.Threading.Thread.Sleep(3000);
                }

            } while (userInput != 3);
        }
    }
}