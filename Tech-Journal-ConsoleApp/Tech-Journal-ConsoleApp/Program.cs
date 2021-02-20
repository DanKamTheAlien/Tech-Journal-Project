using System;

namespace Tech_Journal_ConsoleApp
{
    internal class Program
    {
        public static string UsersName { get; set; }

        private static void Main(string[] args)
        {
            var mode = new Mode();
            Console.WriteLine("Welcome to my Journal app!");
            Console.WriteLine("Please enter your name:");
            UsersName = Console.ReadLine();

            Console.WriteLine($"Hello {UsersName}! Today's Date is {DateTime.Now}");

            mode.Selection();

        }
    }
}