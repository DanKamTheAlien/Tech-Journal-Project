using System;
using System.Collections.Generic;
using System.Text;

namespace Tech_Journal_ConsoleApp
{
    class Mode
    {
        public const string EmailPath = "emailsettings.txt";
        public const string DatabasePath = "databasesettings.txt";
        private static IEntry _journal;
        private static IEmailService _sendEmail;
        private static ICrudService _database;
        private Settings _settings;
        private AppInfo _appInfo;

        public Mode()
        {
            _journal = new Entry();
            _sendEmail = new EmailService();
            _database = new DatabaseService();
            _settings = new Settings();
            _appInfo = new AppInfo();
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
                    _database.ReadEntry(_appInfo);
                    continue;
                }
                if (option == "4")
                {
                    Console.WriteLine("Update Last Journal Entry");
                    var entry = _journal.GetEntryInput();
                    _database.UpdateEntry(_appInfo,entry);
                    continue;
                }
                if (option == "5")
                {
                    Console.WriteLine("Delete Last row");
                    _database.DeleteEntry(_appInfo);
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
            var file = _settings.AppSettingsExists(EmailPath);
            
            if (!file)
            {
                Console.WriteLine("Email Settings were not found. Creating file to store email settings.");
                _appInfo.User = _sendEmail.GetValidEmailAddress(_appInfo.User, "send from:");
                Console.WriteLine("Please enter the password for that email address");
                _appInfo.Password = Console.ReadLine();
                _settings.SaveSenderInfo(_appInfo, EmailPath);
            }
            else
            {
                _appInfo = _settings.ReadSenderInfo(EmailPath);
            }

            var entry = _journal.GetEntryInput();
            _journal.CreateJournalEntry(entry);
            Console.WriteLine("Sending email");
            _sendEmail.ToEmailAddress = _sendEmail.GetValidEmailAddress(_sendEmail.ToEmailAddress, "send to:");
            _sendEmail.SendEmail(_appInfo, _journal.JournalEntries, Program.UsersName, EmailPath);
        }

        public void DatabaseEntry()
        {
            var file = _settings.AppSettingsExists(DatabasePath);
            if (!file)
            {
                Console.WriteLine("Database Settings were not found. Creating file to store Database settings.");
                Console.WriteLine("Please enter the Datasource for that email address");
                _appInfo.Datasource = Console.ReadLine();
                Console.WriteLine("Please enter the Database for that email address");
                _appInfo.Database = Console.ReadLine();
                Console.WriteLine("Please enter the Username for that email address");
                _appInfo.DatabaseUser = Console.ReadLine();
                Console.WriteLine("Please enter the password for that email address");
                _appInfo.DatabasePassword = Console.ReadLine();
                _settings.WriteDatabase(_appInfo, DatabasePath);
            }
            else
            {
                _appInfo = _settings.ReadDatabase(DatabasePath);
            }

            var entry = _journal.GetEntryInput();
            _database.WriteEntry(_appInfo,Program.UsersName, entry);
            _journal.CreateJournalEntry(entry);
            Console.WriteLine("Reading back database");
            _database.ReadEntry(_appInfo);
        }
    }
}
