using System.Collections.Generic;

namespace Tech_Journal_ConsoleApp
{
    internal interface IEmailService
    {
        string ToEmailAddress { get; set; }

        string FromEmailAddress { get; set; }

        string GetValidEmailAddress(string email, string reason);

        void SendEmail(AppInfo info, List<string> entry, string userName, string path);

        bool IsValidEmailAddress(string address);
    }
}