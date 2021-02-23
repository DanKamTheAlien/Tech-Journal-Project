using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Tech_Journal_ConsoleApp;
using System.Net.Mail;

namespace Tech_Journal.Test
{
    [TestFixture]
    public class EmailEntryTests
    {
        public const string Path = "c:\\temp\\unittests.txt";

        [Test]
        public void CheckForEmailSettings_ShouldReturnTrueOnFileExists()
        {
            var sut = new EmailService();
            var file = sut.CheckForEmailSettings(Path);
            Assert.IsTrue(file);
        }

        [Test]
        public void CheckForEmailSettings_ShouldReturnFalseOnFileNotExisting()
        {
            var sut = new EmailService();
            var file = sut.CheckForEmailSettings("c:\\temp\\doesnotexist.txt");
            Assert.IsFalse(file);
        }

        [Test]
        public void IsValidEmailAddress_ShouldReturnTrueOnValidEmail()
        {
            var sut = new EmailService();
            Assert.IsTrue(sut.IsValidEmailAddress("ABC123@gmail.com"));
        }

        [Test]
        public void IsValidEmailAddress_ShouldReturnFalseOnInvalidEmail()
        {
            var sut = new EmailService();
            Assert.IsFalse(sut.IsValidEmailAddress("Abc123"));
        }

        [Test]
        public void ReadSenderEmailSettings_ShouldReturnNotNullWhenReadingFromFile()
        {
            var sut = new EmailService();
            var file = sut.CheckForEmailSettings(Path);
            if (!file)
            {
                sut.GenerateSenderEmailSettings("ABC123@gmail.com", "ABC123", Path);
                sut.ReadSenderEmailSettings(Path);
            }
            else
            {
                sut.ReadSenderEmailSettings(Path);
            }
            Assert.IsNotNull(sut.FromEmailAddress);
            Assert.IsNotNull(sut.EmailPassword);
        }

        [Test]
        public void ReadSenderEmailSettings_ShouldReturnNullWhenReadingFromFile()
        {
            var sut = new EmailService();
            Assert.IsNull(sut.FromEmailAddress);
            Assert.IsNull(sut.EmailPassword);
        }

        [Test]
        public void SendEmail_ShouldThrowExceptionWhenInvalidEmailSettingsAreUsed()
        {
            var sut = new EmailService();
            var sut1 = new List<string> { "test" };
            sut.ToEmailAddress = "fail@gmail.com";
            sut.FromEmailAddress = "fail@gmail.com";
            sut.EmailPassword = "fail@gmail.com";
            Assert.Throws<SmtpException>(() => sut.SendEmail(sut1, "NUnit Test", Path));
        }

    }
}
