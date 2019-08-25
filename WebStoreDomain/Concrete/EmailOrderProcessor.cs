using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDomain.Abstract;

using System.Net;
using System.Net.Mail;
using WebStoreDomain.Entities;

namespace WebStoreDomain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "k.granat2017@gmail.com";
        public string MailFromAddress = "gamestore@example.com";
        public bool UseSsl = true;
        public string Username = "k.granat2017@gmail.com";
        public string Password = "77320426200021d";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"d:\";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

         public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {

            StringBuilder body;
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                 body = new StringBuilder()
                    .AppendLine("Новый заказ обработан")
                    .AppendLine("---")
                    .AppendLine("Товары:");

                foreach (var line in cart.Items)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c}",
                        line.Quantity, line.Product.Name, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Доставка:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Surname)
                    .AppendLine(shippingInfo.Line1)
                    //.AppendLine(shippingInfo.Line2 ?? "")
                    //.AppendLine(shippingInfo.Line3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.Email)
                    .AppendLine(shippingInfo.Phone)               
                    .AppendLine("---")
                    .AppendFormat("Подарочная упаковка: {0}",
                        shippingInfo.GiftWrap ? "Да" : "Нет");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// От кого
                                       emailSettings.MailToAddress,		// Кому
                                       "Новый заказ отправлен!",		// Тема
                                       body.ToString()); 				// Тело письма

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }


                MailAddress from = new MailAddress("eko16p1@gmail.com", "EKO16P1");
                // MailAddress to = new MailAddress("k.granat2017@gmail.com");
                MailAddress to = new MailAddress("k.granat2017@gmail.com");
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Code";
                //  m.Body = "<h2>Письмо-тест работы smtp-клиента</h2><p> tratatatat </p>";
                m.Body = body.ToString();
                m.IsBodyHtml = false;


                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                //"BPD1721@gmail.com", "Q1w2e3r4!"
                smtp.Credentials = new NetworkCredential("eko16p1@gmail.com", "Seti20.09");
                smtp.EnableSsl = true;
                //await smtp.SendMailAsync(m);
                try
                {
                    smtp.Send(m);
                }
                catch (Exception)
                {

                   
                }
               



                //smtpClient.Send(mailMessage);
            }
        }
    }
}
