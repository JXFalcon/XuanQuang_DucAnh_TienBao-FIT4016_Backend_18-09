using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Web_BongDa.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var email = new MimeKit.MimeMessage();
            email.From.Add(MailboxAddress.Parse("email-cua-ban@gmail.com")); // Email gửi
            email.To.Add(MailboxAddress.Parse(toEmail)); // Email nhận
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = message };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            // Kết nối tới server Gmail
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            
            // QUAN TRỌNG: Dùng "Mật khẩu ứng dụng" (App Password) thay vì mật khẩu thật
            await smtp.AuthenticateAsync("email-cua-ban@gmail.com", "ma-16-ky-tu-google-cap");
            
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}