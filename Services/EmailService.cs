using EcommerceBackend.Data;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;


namespace EcommerceBackend.Services
{
    
    public class EmailService
    {
        private readonly DataContext _context;
        private readonly string smtpServer = "smtp.gmail.com";
        private readonly int smtpPort = 587;
        private readonly string smtpUsername = "boye11251@gmail.com";
        private readonly string smtpPassword = "Bo 0++01++0 yes";

        public EmailService(DataContext context)
        {
            _context = context;
        }

        public void SendVerificationEmail(string toEmail, string token)
        {
            var emailTemplate = _context.EmailTemplates.FirstOrDefault();
            if (emailTemplate == null) throw new Exception("Email template not found");

            var verificationLink = $"http://localhost:5000/api/EmailVerification/verify?email={toEmail}&token={token}";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your App", smtpUsername));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = emailTemplate.Subject;
            message.Body = new TextPart("html")
            {
                Text = emailTemplate.Body.Replace("{VerificationLink}", verificationLink)
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(smtpUsername, smtpPassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
