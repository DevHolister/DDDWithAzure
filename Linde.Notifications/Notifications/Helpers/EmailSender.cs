using Linde.Notifications.Coaching.Common.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Notifications.Coaching.Notifications.Helpers
{
    public class EmailSender : IEmailSender
    {
        //public abstract Task ProcessRequestEmail(string to, string subject);
        public async Task Send(string subject, string to, string body)
        {
            var mime = new MimeMessage();
            mime.To.Add(MailboxAddress.Parse(to));
            mime.Subject = subject;
            mime.Body = new TextPart(TextFormat.Html) { Text = body };

            await ProcessBySendEmail(mime);
        }

        private async Task ProcessBySendEmail(MimeMessage mime)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = builder.Build();
            string email = config["Smtp:Username"];
            string password = config["Smtp:Password"];
            int port = int.Parse(config["Smtp:Port"]);
            string host = config["Smtp:Host"];
            string from = config["Smtp:From"];
            string Namefrom = config["Smtp:NameFrom"];

            mime.From.Add(new MailboxAddress(Namefrom, from));

            // send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(host, port, SecureSocketOptions.None);
            await smtp.AuthenticateAsync(email, password);
            await smtp.SendAsync(mime);
            await smtp.DisconnectAsync(true);
        }

        public async Task<string> GetTemplateByName(string template)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = builder.Build();
            string pathtemplate = config["pathTemplate"];
            var directory = pathtemplate;
            var path = Path.Combine(directory, $"{template}.html");

            if (string.IsNullOrEmpty(path)) return string.Empty;

            return await ReadDocument(path);
        }
        protected async Task<string> ReadDocument(string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;

            return string.Join(Environment.NewLine, await File.ReadAllLinesAsync(path));
        }
    }
}
