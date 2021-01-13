using System.Threading.Tasks;
using ContractBookShop;
using MassTransit;
using Task2WebApplication.Services;

namespace Task2WebApplication.Consumer
{
    public class BooksResponseConsumer: IConsumer<IBooksResponse>
    {
        private readonly BookShop _bookShop;

        public BooksResponseConsumer(BookShop bookShop)
        {
            _bookShop = bookShop;
        }
        public async Task Consume(ConsumeContext<IBooksResponse> context)
        {
            var message = context.Message;
            
            await _bookShop.ReceiveDelivery(message.Books);
        }  
    }
}