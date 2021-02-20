using System;
using System.Collections.Generic;
using System.Text;

namespace Tech_Journal_ConsoleApp
{
    class Mode
    {
        public const string Path = "appsettings.txt";
        private static IJournal _journal;
        private static IEmailEntry _sendEmail;
        private static IJournalDatabase _database;

        public Mode()
        {
            _journal = new Journal();
            _sendEmail = new EmailEntry();
            _database = new JournalDatabase();
        }
        public void Selection()
        {
            var selection = false;
            Console.WriteLine("How would you like to save your journal entry?");
            while (selection != true)
            {
                Console.WriteLine("Enter 1 for sending the entry via email");
                Console.WriteLine("Enter 2 for saving the journal entry in a database");
                Console.WriteLine("Enter 3 to read back all journal entries");
                Console.WriteLine("Enter 4 to update the last journal entry");
                Console.WriteLine("Enter 5 to delete the last journal entry");
                Console.WriteLine("Enter q to quit the application");
                var option = Console.ReadLine();
                if (option == "1")
                {
                    EmailEntry();
                    continue;
                }

                if (option == "2")
                {
                    DatabaseEntry();
                    continue;
                }
                if (option == "3")
                {
                    Console.WriteLine("Reading back database");
                    _database.ReadEntryDatabase();
                    continue;
                }
                if (option == "4")
                {
                    Console.WriteLine("Update Last Journal Entry");
                    var entry = _journal.GetEntryInput();
                    _database.UpdateEntryDatabase(entry);
                    continue;
                }
                if (option == "5")
                {
                    Console.WriteLine("Delete Last row");
                    _database.DeleteEntryDatabase();
                    continue;
                }
                if (option != null && option.ToLower() == "q")
                {
                    break;
                }

                Console.WriteLine("You did not select a correct option");
            }
        }

        public void EmailEntry()
        {
            var file = _sendEmail.CheckForEmailSettings(Path);
            
            if (!file)
            {
                Console.WriteLine("Email Settings were not found. Creating file to store email settings.");
                var fromEmailAddress = _sendEmail.GetValidEmailAddress(_sendEmail.FromEmailAddress, "send from:");
                Console.WriteLine("Please enter the password for that email address");
                var emailPassword = Console.ReadLine();
                _sendEmail.GenerateSenderEmailSettings(fromEmailAddress, emailPassword, Path);
                _sendEmail.ReadSenderEmailSettings(Path);
            }
            else
            {
                _sendEmail.ReadSenderEmailSettings(Path);
            }

            var entry = _journal.GetEntryInput();
            _journal.CreateJournalEntry(entry);
            Console.WriteLine("Sending email");
            _sendEmail.ToEmailAddress = _sendEmail.GetValidEmailAddress(_sendEmail.ToEmailAddress, "send to:");
            _sendEmail.SendEmail(_journal.JournalEntries, Program.UsersName, Path);
        }

        public void DatabaseEntry()
        {
            var entry = _journal.GetEntryInput();
            _database.WriteEntryDatabase(Program.UsersName, entry);
            _journal.CreateJournalEntry(entry);
            Console.WriteLine("Reading back database");
            _database.ReadEntryDatabase();
        }
    }
}
