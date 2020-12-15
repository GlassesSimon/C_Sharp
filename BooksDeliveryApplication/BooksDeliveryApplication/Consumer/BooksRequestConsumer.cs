using System;
using System.Threading.Tasks;
using ContractBookShop;
using MassTransit;

namespace BooksDeliveryApplication.Consumer
{
    public class BooksRequestConsumer: IConsumer<IBooksRequest>
    {
        public Task Consume(ConsumeContext<IBooksRequest> context)
        {
            var message = context.Message;
            Console.WriteLine($"Number of books is {message.BooksCount}");
            return Task.CompletedTask;
        }
    }
}