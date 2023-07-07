﻿using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common.Utilities
{
    public class EmailService : IEmailService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly bool _useSSl;
        private readonly string _from;
        public EmailService(string host, int port, string username, string password, bool useSSl, string from)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
            _useSSl = useSSl;
            _from = from;
        }
        public void SendEmail(string receive, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_from, _from));
            message.To.Add(new MailboxAddress(receive, receive));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using var client = new SmtpClient();
            client.Timeout = 60000; 
            client.Connect(_host, _port, _useSSl);
            client.Authenticate(_username, _password);
            client.Send(message);
            client.Disconnect(true);
           
        }
    }
}
