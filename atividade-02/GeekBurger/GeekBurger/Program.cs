using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace GeekBurger
{
    class Program
    {
        const string QueueConnectionString = "l+/ahOyFlKJ2BduLmg/1i1RPGRj0gjt/dtA4GuHgnAg=";
        const string QueuePath = "productchanged";
        static IQueueClient _queueClient;


        static void Main(string[] args)
        {
            new Program().Start(args).GetAwaiter().GetResult();

        }

        private async Task Start(string[] args)
        {
            if (args.Length <= 0 || args[0] == "sender")
            {
                await SendMessagesAsync();
                Console.WriteLine("messages were sent");
            }
            else if (args[0] == "receiver")
            {
                await ReceiveMessagesAsync();
                Console.WriteLine("messages were received");
            }
            else
            {
                Console.WriteLine("nothing to do");
            }

            Console.ReadLine();
        }

        private async Task SendMessagesAsync()
        {
            _queueClient = new QueueClient(QueueConnectionString, QueuePath);
            var messages = "Hi,Hello,Hey,How are you,Be Welcome"
                .Split(',')
                .Select(msg =>
                {
                    Console.WriteLine($"Will send message: {msg}");
                    return new Message(Encoding.UTF8.GetBytes(msg));
                }).ToList();

            await _queueClient.SendAsync(messages);
            await _queueClient.CloseAsync();
        }

        private async Task ReceiveMessagesAsync()
        {
            _queueClient = new QueueClient(QueueConnectionString, QueuePath);
            _queueClient.RegisterMessageHandler(MessageHandler, new MessageHandlerOptions(ExceptionHandler)
            {
                AutoComplete = false
            });

            Console.ReadLine();
            await _queueClient.CloseAsync();
        }

        private async Task MessageHandler(Message message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Received message:{Encoding.UTF8.GetString(message.Body)}");
            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionHandler(ExceptionReceivedEventArgs exceptionArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionArgs.Exception}.");
            var context = exceptionArgs.ExceptionReceivedContext;
            Console.WriteLine($"Endpoint:{context.Endpoint}, Path:{context.EntityPath}, Action:{context.Action}");
            return Task.CompletedTask;
        }
    }
}
