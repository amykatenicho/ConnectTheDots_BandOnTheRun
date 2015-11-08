using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace BandDemoOutput
{
    class Program
    {
        static void Main(string[] args)
        {

            string ehName = "<eventhubname>";
            string connection = "Endpoint=sb://<servicebusnamespace>.servicebus.windows.net/;SharedAccessKeyName=<policyname>;SharedAccessKey=<policykey>;TransportType=Amqp";
            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(connection);
            EventHubClient ehub = factory.CreateEventHubClient(ehName);
            EventHubConsumerGroup group = ehub.GetDefaultConsumerGroup();
            EventHubReceiver reciever = group.CreateReceiver("0");

            while (true)
            {
                EventData data = reciever.Receive();
                if (data != null)
                {
                    try
                    {
                        string message = Encoding.UTF8.GetString(data.GetBytes());
                        Console.WriteLine(message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }
    }
}
