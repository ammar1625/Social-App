using MailKit.Net.Smtp;
using MimeKit;

namespace ApiAuthenticationAndSecurity.User_Management
{
    public class EmailService : IemailService
    {
        private readonly EmailConfigurations _configuration;

        public EmailService(EmailConfigurations emailConfigurations)
        {
            _configuration = emailConfigurations;
        }
        public void SendEmail(message EmailMessage)
        {
           var mimeMessage = _CreateMimeMessage(EmailMessage);
           _Send(mimeMessage);
        }

        private MimeMessage _CreateMimeMessage(message message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("email", _configuration.From));
            mimeMessage.To.AddRange(message.To);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return mimeMessage;
        }

        private void _Send(MimeMessage message)
        {
            using(var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration.SmtpServer , _configuration.Port , true);
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_configuration.UserName, _configuration.PassWord);

                    client.Send(message);
                }
                catch (Exception ex) 
                {
                    //handle error here 
                }
                finally 
                {
                    client.Disconnect(true);
                    client?.Dispose();
                }
            }
        }
    }
}
