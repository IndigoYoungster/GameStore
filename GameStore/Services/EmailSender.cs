using GameStore.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Services
{
    public class EmailSender
    {
        public void SendEmailAsync(OrderModel order, List<CartItemModel> cartItems)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("NiStore", "***"));
            emailMessage.To.Add(new MailboxAddress("", order.Email));
            emailMessage.Subject = "Purchase";

            StringBuilder message = new StringBuilder();
            message.Append($"{order.Name}, thanks for the purchase!\n" +
                           $"Total cost: {order.TotalCost}\n" +
                           $"Your order:\n");

            int i = 1;
            foreach (var item in cartItems)
            {
                message.Append($"   {i}. {item.Game.Name} - ({item.Count})\n");
                i++;
            }

            emailMessage.Body = new TextPart("Plain")
            {
                Text = message.ToString()
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("***", "***");
                client.Send(emailMessage);

                client.Disconnect(true);
            }
        }
    }
}
