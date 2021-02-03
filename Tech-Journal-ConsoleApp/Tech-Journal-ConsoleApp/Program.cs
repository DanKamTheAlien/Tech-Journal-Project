using System;
using System.Reflection.Metadata;

namespace Tech_Journal_ConsoleApp
{
    internal class Program
    {
        public const string Path = "appsettings.txt";
        private static IJournal _journal;
        private static IEmailEntry _sendEmail;
        private static void Main(string[] args)
        {
            _journal = new Journal();
            _sendEmail = new EmailEntry();

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

            Console.WriteLine("Please enter your name:");
            var userName = Console.ReadLine();
            Console.WriteLine($"Hello {userName}! Today's Date is {DateTime.Now}");
            var entry = _journal.GetEntryInput();
            _journal.CreateJournalEntry(entry);
            Console.WriteLine("Sending email");
            _sendEmail.ToEmailAddress = _sendEmail.GetValidEmailAddress(_sendEmail.ToEmailAddress, "send to:");
            _sendEmail.SendEmail(_journal.JournalEntries, userName, Path);
        }
    }
}