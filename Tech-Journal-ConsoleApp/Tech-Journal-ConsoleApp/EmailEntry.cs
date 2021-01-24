using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Tech_Journal_ConsoleApp
{
    public class EmailEntry
    {
        public const string Path = "c:\\temp\\appsettings.txt";

        public string ToEmailAddress { get; set; }

        public string FromEmailAddress { get; set; }

        public string EmailPassword { get; set; }

        public EmailEntry()
        {
            
        }

        public bool CheckForEmailSettings()
        {
            return File.Exists(Path);
        }

        public string GetValidEmailAddress(string email,string reason)
        {
            while (!IsValidEmailAddress(email))
            {
                Console.WriteLine($"Please enter a valid email address to {reason}");
                email = Console.ReadLine();
            }
            return email;
        }

        public void GenerateSenderEmailSettings(string fromEmailAddress, string emailPassword)
        {
            using var sw = File.AppendText(Path);
            sw.WriteLine(fromEmailAddress);
            sw.WriteLine(emailPassword);
        }

        public void ReadSenderEmailSettings()
        {
            using var emailSettings = new StreamReader(Path);
            FromEmailAddress = emailSettings.ReadLine();
            EmailPassword = emailSettings.ReadLine();
        }
        
        public void SendEmail( List<string> entry, string userName)
        {
            var combinedJournalEntries= string.Join(",", entry);

            try
            {
                var message = new MailMessage(FromEmailAddress, ToEmailAddress)
                {
                    Subject = $"JournalEntries Entry for {userName},{DateTime.Now:yyyy-MM-dd}",
                    Body = combinedJournalEntries
                };
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(FromEmailAddress, EmailPassword),
                    EnableSsl = true,
                };
                smtpClient.Send(message);

            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                File.Delete(Path);
                throw;
            }
        }
        public bool IsValidEmailAddress(string address) => address != null && new EmailAddressAttribute().IsValid(address);

    }
}
