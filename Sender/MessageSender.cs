using Sender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    public class MessageSender : IMessageSender
    {
        private MessageContainer messages;
        private MessageCore MessageCore;
        private readonly string phoneNumber;


        public MessageSender(
            string phoneNumber,
            string apiKey,
            string apiSecret,
            string domain = "api.solapi.com",
            string protocol = "https",
            string prefix = ""
            )
        {
            this.phoneNumber = phoneNumber;
            messages = new MessageContainer();
            MessageCore = new MessageCore(
                    apiKey,
                    apiSecret,
                    domain,
                    protocol,
                    prefix
                );
        }

        public async Task<bool> SendEmailAsync(string toPhoneNumber, string text)
        {
            messages.Add(new Message()
            {
                To = toPhoneNumber,
                From = phoneNumber,
                Text = text
            });

            Response response = await Task.Run ( () => MessageCore.SendMessages(messages));

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("전송 결과");
                Console.WriteLine("Group ID:" + response.Data.SelectToken("groupId").ToString());
                Console.WriteLine("Status:" + response.Data.SelectToken("status").ToString());
                Console.WriteLine("Count:" + response.Data.SelectToken("count").ToString());
            }
            else
            {
                Console.WriteLine("Error Code:" + response.ErrorCode);
                Console.WriteLine("Error Message:" + response.ErrorMessage);
            }

            return response.StatusCode == System.Net.HttpStatusCode.OK;

        }
    }
}
