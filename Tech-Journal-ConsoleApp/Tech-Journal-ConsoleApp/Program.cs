using System;

namespace Tech_Journal_ConsoleApp
{
    internal class Program
    {
        public const string Path = "appsettings.txt";
        private static IJournal _journal;
        private static IEmailEntry _sendEmail;
        private static IJournalDatabase _database;

        private static void Main(string[] args)
        {
            _journal = new Journal();
            _sendEmail = new EmailEntry();
            _database = new JournalDatabase();
            var mode = new Mode();

            var file = _sendEmail.CheckForEmailSettings(Path);
            Console.WriteLine("Welcome to my Journal app!");
            Console.WriteLine("Please enter your name:");
            var userName = Console.ReadLine();
            Console.WriteLine($"Hello {userName}! Today's Date is {DateTime.Now}");

            var userSelection = mode.Selection();
            if (userSelection == 1)
            {
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
                _sendEmail.SendEmail(_journal.JournalEntries, userName, Path);
            }

            if (userSelection == 2)
            {
                var entry = _journal.GetEntryInput();
                _database.WriteEntryDatabase(userName, entry);
                _journal.CreateJournalEntry(entry);
                Console.WriteLine("Reading back database");
                _database.ReadEntryDatabase();
                Console.WriteLine("Delete Last row");
                _database.DeleteEntryDatabase();
                _database.ReadEntryDatabase();
            }
        }
    }
}