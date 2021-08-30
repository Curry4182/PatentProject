using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using Patent.Models;

namespace Patent.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string fromMail;
        private readonly string fromPassword;
        public EmailSender(string fromMail, string fromPassword)
        {
            this.fromMail = fromMail;
            this.fromPassword = fromPassword;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;

            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> " + htmlMessage + "</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            await Task.Run ( () => smtpClient.Send(message));
        }
    }
}
