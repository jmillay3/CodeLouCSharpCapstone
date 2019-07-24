using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace __WhereIsMy__.App.Menus
{

    public class ManageMenu
    {
        public static string _dataFile;

        //get the DB directory and json file
        public static Delta.StuffDelta _stuffDelta = new Delta.StuffDelta($@"{Program.JsonDataDirectory}Data\");

        public static List<Stuffs.Stuff> _stuffList { get; set; }


        public static int DisplayMenu()
        {
            //print the menu to screen
            //vulgar menu
            //changed back to not so vulgar... >:(
            Console.Clear();
            Console.WriteLine(" OK, here it is...step up to the plate and MANAGE YOUR STUFF, CHUMP!");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" 1. List My Stuff...");
            Console.WriteLine(" 2. Add More Stuff...");
            Console.WriteLine(" 3. Get Rid of Some Stuff...(Delete)");
            Console.WriteLine(" 4. Return to the Main Menu...");
            Console.WriteLine();
            Console.Write("Decisions, decisions...what you got Captain Dingleberry? ");

            //snag the response
            var response = Console.ReadLine();
            return Convert.ToInt32(response);
        }

        public static void Run()
        {
            //create a var to hold the user's selection
            int userInput = 0;

            //continue to loop until a valid option is chosen
            do
            {
                //get the selection
                userInput = DisplayMenu();

                //perform an action based on a selection
                switch (userInput)
                {
                    case 1:
                        GetAll();
                        break;
                    case 2:
                        Add();
                        break;
                    case 3:
                        Delete();
                        break;
                    case 4:
                        MainMenu.DisplayMenu();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine(" Error: Invalid Choice");
                        System.Threading.Thread.Sleep(1000);
                        break;
                }

            } while (userInput != 4);
        }

        public static void GetAll()
        {            
            //get the list of stuff from the service
            _stuffList = _stuffDelta.GetAll();

            //create a DisplayTable object for displaying output like a table
            DisplayTable stuff_ct = new DisplayTable(111);

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" YOUR STUFF");
            stuff_ct.PrintLine();

            string[] headers = new[] { "ID","Stuff","Location","Last Known Date" };
            
            //Console.WriteLine(_stuffList);

            if (_stuffList.Any())
            {
                stuff_ct.PrintRow(headers);
                stuff_ct.PrintLine();

                foreach (var stuff in _stuffList)
              {
                string[] rowData = new[] { stuff.Stuff_ID.ToString(), stuff.Stuff_Name, stuff.Location_Name, stuff.Location_Date.ToString() };
                stuff_ct.PrintRow(rowData);

              }
            }
            else
            {
              Console.WriteLine(" This is odd...you have no stuff. Try adding some of your junk cause I know you have it.");
            }

            stuff_ct.PrintLine();

            Console.WriteLine();
            Console.WriteLine(" Press [enter] to return to the previous menu.");
            Console.ReadLine();
        }

        private static void Add()
        {
            //get the existing list of stuffs
            _stuffList = _stuffDelta.GetAll();

            //instantiate the new stuff object
            Stuffs.Stuff stuff = new Stuffs.Stuff();

            //prompt the user for the stuff name
            Console.WriteLine();
            Console.Write("What is this stuff you're trying to add? What's it called? ");
            //read the input from the console as the stuff name
            stuff.Stuff_Name = Console.ReadLine();

            //prompt the user for the location name
            Console.WriteLine();
            Console.WriteLine(">:| ...That's nice...");
            Console.WriteLine();
            Console.Write("Where is that stuff at? ");
            //read the input from the console as the location name
            stuff.Location_Name = Console.ReadLine();

            //get the current date and time for Location_Date
            stuff.Location_Date = DateTime.Now;

            //call the stuff delta and add the stuff
            stuff = _stuffDelta.Add(stuff, _stuffList);

            //give the user a confirmation--pause for a few seconds
            Console.WriteLine();
            Console.WriteLine("Good to know...good to know...");
            Console.WriteLine();
            Console.WriteLine($"Got it! Added new stuff ID: {stuff.Stuff_ID}");
            System.Threading.Thread.Sleep(3000);
        }

        private static void Delete()
        {
            //collect the id of the stuff to delete
            Console.Write("ID of stuff to get rid of: ");
            int.TryParse(Console.ReadLine(), out var stuffID);

            //get the list of stuff
            _stuffList = _stuffDelta.GetAll();
            var stuffToRemove = _stuffList.SingleOrDefault(s => s.Stuff_ID == stuffID);

            //make sure stuff with that id exists before attempting to remove it
            if (stuffToRemove != null)
            {
                //remove the stuff
                _stuffDelta.Delete(stuffToRemove, _stuffList);

                //give response to the user and pause for a few seconds
                Console.WriteLine();
                Console.Write($"Success! Finally, you got rid of Stuff ID: {stuffID} and it was deleted.");
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                //could not find the stuff so show an ERROR and pause for a few seconds
                Console.Write($"There's NOT any stuff with ID: {stuffID}.");
                System.Threading.Thread.Sleep(3000);
            }
        }

    }
}
