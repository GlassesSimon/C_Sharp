using System;
using System.Threading.Tasks;
using BooksDeliveryApplication.Services;
using ContractBookShop;
using MassTransit;

namespace BooksDeliveryApplication.Consumer
{
    public class BooksRequestConsumer: IConsumer<IBooksRequest>
    {
        private readonly IServiceProxy _serviceProxy;

        public BooksRequestConsumer(IServiceProxy serviceProxy)
        {
            _serviceProxy = serviceProxy;
        }
        public Task Consume(ConsumeContext<IBooksRequest> context)
        {
            var message = context.Message;
            Console.WriteLine($"Number of books is {message.BooksCount}");
            _serviceProxy.GetAndSaveBooks(message.BooksCount);
            return Task.CompletedTask;
        }
    }
}