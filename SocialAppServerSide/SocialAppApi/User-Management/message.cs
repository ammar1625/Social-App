using MimeKit;

namespace ApiAuthenticationAndSecurity.User_Management
{
    public class message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public message(IEnumerable<string>to , string subject ,string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(adress=>new MailboxAddress("email",adress)));
            Subject = subject;
            Content = content;
        }
    }
}
