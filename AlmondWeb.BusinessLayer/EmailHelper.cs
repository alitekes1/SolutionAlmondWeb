using AlmondWeb.Entities;
using System.Net;
using System.Net.Mail;
namespace AlmondWeb.BusinessLayer
{
    public static class EmailHelper
    {
        public static void SendEmail(AlmondUserTable user, string messageBody, string messageSubject)
        {
            string apiKey = "8JXGhtbgvf045yqB";
            // E-posta gönderen bilgileri
            string fromName = "AlmondWeb";
            string fromEmail = "alitekes123@gmail.com";

            // E-posta konusu ve içeriği
            string subject = messageSubject;
            string body = messageBody;

            // Alıcı listesi
            string[] recipients = { user.Email };

            // SMTP sunucu ayarları
            SmtpClient smtpClient = new SmtpClient("smtp-relay.brevo.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, apiKey),
                EnableSsl = true,
            };

            // E-posta gönderme işlemi
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail, fromName);
            foreach (string recipient in recipients)
            {
                mail.To.Add(recipient);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            smtpClient.Send(mail);
        }
    }
}
