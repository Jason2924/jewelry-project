using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Project_Sem_3.Models
{
    public class Mail
    {
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Mail() { }
        public Mail(string receiver, string subject, string content)
        {
            this.Receiver = receiver;
            this.Subject = subject;
            this.Content = content;
        }

        public void BuildMail()
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("yashgems.jewelry@gmail.com");
            mail.To.Add(new MailAddress(Receiver));
            mail.Body = Content;
            mail.Subject = Subject;
            SendMail(mail);
        }

        public void SendMail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("yashgems.jewelry@gmail.com", "Yash440119");
            client.Send(mail);
        }

    }
}