using System.Threading.Tasks;
using ApplicationServices;
using ContractBookShop;
using MassTransit;

namespace Task2WebApplication.Consumer
{
    public class BooksResponseConsumer: IConsumer<IBooksResponse>
    {
        private BookShop _bookShop;

        public BooksResponseConsumer(BookShop bookShop)
        {
            _bookShop = bookShop;
        }
        public Task Consume(ConsumeContext<IBooksResponse> context)
        {
            var message = context.Message;
            return Task.CompletedTask;
        }  
    }
}