using Microsoft.Extensions.Configuration;
using Repositories;
using Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Services.Service
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;
        public EmailSenderService(IConfiguration configuration) => _configuration = configuration;

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var stmpClient = new SmtpClient
            {
                Host = _configuration["Email:SmtpServer"],
                Port = int.Parse(_configuration["Email:Port"]),
                EnableSsl = true,
                Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:From"]),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);
            await stmpClient.SendMailAsync(mailMessage);
        }
    }
}
