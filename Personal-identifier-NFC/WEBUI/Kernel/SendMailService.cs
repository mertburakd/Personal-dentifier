using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WEBUI.Kernel
{
    public class SendMailService
    {
        public static bool sendMail(string toMail, string bodyHtml, string subject, string filePath = null, string folderName = null)
        {
            string pass = "lchdpzkuabbfubif";
            var smtpClient = new SmtpClient("smtp.yandex.com.tr")
            {
                Port = 587,
                Credentials = new NetworkCredential("helpcenter@sugomigames.com", pass),
                EnableSsl = true,
            };

            MailMessage _Message = new MailMessage();
            _Message.To.Add(toMail);
            if (filePath != null && folderName != null)
            {
                byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                _Message.Attachments.Add(new Attachment(new MemoryStream(bytes), folderName));
            }
            _Message.IsBodyHtml = true;
            _Message.Subject = subject;
            _Message.From = new MailAddress("helpcenter@sugomigames.com");
            _Message.Body = bodyHtml;

            try
            {
                smtpClient.Send(_Message);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }

    }
}
