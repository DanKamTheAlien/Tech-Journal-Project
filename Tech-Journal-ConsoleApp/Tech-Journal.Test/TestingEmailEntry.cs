using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Tech_Journal_ConsoleApp;
using System.Net.Mail;

namespace Tech_Journal.Test
{
    [TestFixture]
    public class TestingEmailEntry
    {
        [Test]
        public void ValidateEmailInput()
        {
            var sut = new EmailEntry();
            Assert.IsTrue(sut.IsValidEmailAddress("ABC123@gmail.com"));
            Assert.IsFalse(sut.IsValidEmailAddress("Abc123"));
        }
        [Test]
        public void ValidateFromEmailLoads()
        {
            var sut = new EmailEntry();
            var file = sut.CheckForEmailSettings();
            if (!file)
            {
                sut.GenerateSenderEmailSettings("ABC123@gmail.com", "ABC123");
            }
            else
            {
                sut.ReadSenderEmailSettings();
            }
            Assert.IsNotNull(sut.FromEmailAddress);
        }
        [Test]
        public void ValidateFromEmailPasswordLoads()
        {
            var sut = new EmailEntry();
            var file = sut.CheckForEmailSettings();
            if (!file)
            {
                sut.GenerateSenderEmailSettings("ABC123@gmail.com", "ABC123");
            }
            else
            {
                sut.ReadSenderEmailSettings();
            }
            Assert.IsNotNull(sut.EmailPassword);
        }
        [Test]
        public void ValidateEmailSendsFailsWhenIncorrect()
        {
            var sut = new EmailEntry();
            var sut1 = new List<string> { "test" };
            sut.ToEmailAddress = "fail@gmail.com";
            sut.FromEmailAddress = "fail@gmail.com";
            sut.EmailPassword = "fail@gmail.com";
            Assert.Throws<SmtpException>(() => sut.SendEmail(sut1, "NUnit Test"));
        }

    }
}
