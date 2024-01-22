using System.Net;
using System.Net.Mail;
using System.Text;

namespace AFC.Business.Service;

/// <summary>
/// Sistem'de ödeme simülasyonunda mail atar.
/// </summary>
public interface INotificationService
{
    Task SendEmail(string Subject, string Email, string Content);
}
public class NotificationService : INotificationService
{
    const string gmail_account = "akbankfinalcase@gmail.com";
    const string gmail_password = "wycj fdfd kvep beth";

    public async Task SendEmail(string Subject, string Email, string Content)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        smtpClient.EnableSsl = true;

        smtpClient.Credentials = new NetworkCredential(gmail_account, gmail_password);

        MailAddress from = new MailAddress(gmail_account, "Ahmet Er Akbank Final Case");
        MailAddress to = new MailAddress(Email);
        MailMessage mail = new MailMessage(from, to);

        mail.Subject = Subject;
        mail.SubjectEncoding = Encoding.UTF8;

        mail.Body = $@"<html><body><p>{Content}</p></body></html>";
        mail.BodyEncoding = Encoding.UTF8;
        mail.IsBodyHtml = true;

        smtpClient.Send(mail);
    }
}
