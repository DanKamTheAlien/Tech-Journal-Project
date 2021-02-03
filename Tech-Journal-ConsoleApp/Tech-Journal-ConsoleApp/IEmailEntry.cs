using System.Collections.Generic;

namespace Tech_Journal_ConsoleApp
{
    internal interface IEmailEntry
    {
        string ToEmailAddress { get; set; }

        string FromEmailAddress { get; set; }

        string EmailPassword { get; set; }

        bool CheckForEmailSettings(string path);

        string GetValidEmailAddress(string email, string reason);

        void GenerateSenderEmailSettings(string fromEmailAddress, string emailPassword, string path);

        void ReadSenderEmailSettings(string path);

        void SendEmail(List<string> entry, string userName, string path);

        bool IsValidEmailAddress(string address);
    }
}