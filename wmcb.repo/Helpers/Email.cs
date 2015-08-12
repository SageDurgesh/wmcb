using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using wmcb.model;

namespace wmcb.repo.Helpers
{
    public class Email
    {
        public static void SendEmail(EmailMessage msg)
        {           
            MailMessage EmailMsg = new MailMessage();
            EmailMsg.From = new MailAddress("web@wmcbleague.org");
            EmailMsg.To.Add(new MailAddress(msg.EmailAddress));
            EmailMsg.Subject = msg.Subject;
            EmailMsg.Body = msg.Body;
            EmailMsg.IsBodyHtml = true;
            EmailMsg.Priority = MailPriority.Normal;
            EmailMsg.BodyEncoding = Encoding.UTF8;
            SmtpClient MailClient = new SmtpClient("mail.wmcbleague.org",587);
            MailClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            MailClient.Credentials = new System.Net.NetworkCredential("web@wmcbleague.org", "Cricket2015");
            MailClient.Send(EmailMsg);
        }
    }
}
