using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Tech_Journal_ConsoleApp
{
    public class EmailService : IEmailService
    {
        public string ToEmailAddress { get; set; }
        public string FromEmailAddress { get; set; }




        public string GetValidEmailAddress(string email, string reason)
        {
            while (!IsValidEmailAddress(email))
            {
                Console.WriteLine($"Please enter a valid email address to {reason}");
                email = Console.ReadLine();
            }

            return email;
        }

        public void SendEmail(AppInfo info, List<string> entry, string userName, string path)
        {
            var combinedJournalEntries = string.Join(",", entry);

            try
            {
                var message = new MailMessage(info.User, ToEmailAddress)
                {
                    Subject = $"JournalEntries Entry for {userName},{DateTime.Now:yyyy-MM-dd}",
                    Body = combinedJournalEntries
                };
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(info.User, info.Password),
                    EnableSsl = true
                };
                smtpClient.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                File.Delete(path);
                throw;
            }
        }

        public bool IsValidEmailAddress(string address)
        {
            return address != null && new EmailAddressAttribute().IsValid(address);
        }
    }
}