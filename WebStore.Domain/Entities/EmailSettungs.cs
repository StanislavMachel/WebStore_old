using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Abstract;
using System.Net.Mail;
using System.Net;

namespace WebStore.Domain.Entities
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "webstore@examlpe.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WrireAsFile = false;
        public string FileLocation = @"D:\\CSharp\\debug";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
                if (emailSettings.WrireAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("------------------------------")
                    .AppendLine("Items:");
                foreach (var productQuantity in cart.ProductQuantityCollection)
                {
                    var subtotal = productQuantity.Product.Price * productQuantity.Product.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal : {2:c}",
                        productQuantity.Quantity,
                        productQuantity.Product.Name,
                        subtotal);
                }

                body.AppendFormat("Total order value {0:c}", cart.ComputeTotalCost())
                    .AppendLine("------------------------------")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2 ?? "")
                    .AppendLine(shippingDetails.Line3 ?? "")
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.State ?? "")
                    .AppendLine(shippingDetails.Country)
                    .AppendLine(shippingDetails.Zip)
                    .AppendLine("------------------------------")
                    .AppendFormat("Gift wrap: {0}", shippingDetails.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage
                    (
                        emailSettings.MailFromAddress,
                        emailSettings.MailToAddress,
                        "New order submitted!",
                        body.ToString()
                    );

                if (emailSettings.WrireAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
            }
        }
    }
}
