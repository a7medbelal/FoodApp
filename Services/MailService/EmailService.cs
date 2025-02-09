
using FoodApp.Core.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;


namespace FoodApp.Services.MailService
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings mailSettings;  
        public EmailService(IOptions<MailSettings > options ) {
         mailSettings = options.Value;  
        }
        public async Task SendEmailAsync(string MailTo, string Subject, string Body)
        {
            var Email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(mailSettings.Email),
                Subject = Subject

            };
             Email.To.Add(MailboxAddress.Parse(MailTo));


            var Builder = new BodyBuilder();

            Builder.HtmlBody = Body; 

            Email.Body = Builder.ToMessageBody();
            Email.From.Add(new MailboxAddress (mailSettings.DisplayNAme , mailSettings.Email));


            using var Smtp = new SmtpClient();

            Smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);

            Smtp.Authenticate(mailSettings.Email, mailSettings.Password);

            await Smtp.SendAsync(Email);

            Smtp.Disconnect(true);
        }
    }
}
