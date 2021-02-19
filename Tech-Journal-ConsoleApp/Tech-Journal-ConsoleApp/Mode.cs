using System;
using System.Collections.Generic;
using System.Text;

namespace Tech_Journal_ConsoleApp
{
    class Mode
    {
        public int Selection()
        {
            var selection = false;
            Console.WriteLine("How would you like to save your journal entry?");
            while (selection != true)
            {
                Console.WriteLine("Enter 1 for sending the entry via email");
                Console.WriteLine("Enter 2 for saving the journal entry in a database");
                var option = Console.ReadLine();
                if (option == "1"){
                    return 1;
                    break;
                }
                if (option == "2")
                {
                    return 2;
                    break;
                }
                Console.WriteLine("You did not select a correct option");
            }
            return 0;
        }
    }
}
