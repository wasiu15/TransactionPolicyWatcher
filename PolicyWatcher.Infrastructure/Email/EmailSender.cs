using System.Net.Mail;
using System.Net;

namespace PolicyWatcher.Infrastructure.Email
{
    public static class EmailSender
    {
        public static bool SendEmail(string message)
        {
            string fromAddress = "abiolawasiu.code@gmail.com";
            string password = "sphb wgyc veiw qvzh";
            string toAddress = "abiola.wasiu039@gmail.com";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromAddress, password),
                EnableSsl = true,
            };
            MailMessage mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Policy Watcher Notification",
                Body = message,
                IsBodyHtml = false,
            };

            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
            return false;
        }
    }
}
