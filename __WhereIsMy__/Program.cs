using System;

namespace __WhereIsMy__.App
{
    class Program
    {
        //Here is the Data File
        public static string JsonDataDirectory { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        static void Main(string[] args)
        {
            Menus.MainMenu.Run();
        }
    }
}
