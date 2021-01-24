using System;

namespace Tech_Journal_ConsoleApp
{
    internal class Program
    {
        public const string Path = "c:\\temp\\appsettings.txt";
        private static void Main(string[] args)
        {
            var journal = new Journal();
            var sendEmail = new EmailEntry();
            var file = sendEmail.CheckForEmailSettings(Path);
            if (!file)
            {
                Console.WriteLine("Email Settings were not found. Creating file to store email settings.");
                var fromEmailAddress = sendEmail.GetValidEmailAddress(sendEmail.FromEmailAddress, "send from:");
                Console.WriteLine("Please enter the password for that email address");
                var emailPassword = Console.ReadLine();
                sendEmail.GenerateSenderEmailSettings(fromEmailAddress, emailPassword, Path);
            }
            else
            {
                sendEmail.ReadSenderEmailSettings(Path);
            }
            Console.WriteLine("Please enter your name:");
            var userName = Console.ReadLine();
            Console.WriteLine($"Hello {userName}! Today's Date is {DateTime.Now}");
            var entry = journal.GetEntryInput();
            journal.CreateJournalEntry(entry);
            Console.WriteLine("Sending email");
            sendEmail.ToEmailAddress = sendEmail.GetValidEmailAddress(sendEmail.ToEmailAddress, "send to:");
            sendEmail.SendEmail(journal.JournalEntries, userName, Path);
        }
    }
}
