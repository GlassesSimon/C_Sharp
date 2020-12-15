using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ContractBookShop;
using MassTransit;
using Task2;
using Task2WebApplication.Services;

namespace Task2WebApplication.Consumer
{
    public class BooksResponseConsumer: IConsumer<IBooksResponse>
    {
        private readonly BookShop _bookShop;
        private readonly Mapper _mapper;

        public BooksResponseConsumer(BookShop bookShop, Mapper mapper)
        {
            _bookShop = bookShop;
            _mapper = mapper;
        }
        public Task Consume(ConsumeContext<IBooksResponse> context)
        {
            var message = context.Message;
            var books = _mapper.Map<List<IBooksResponse.Book>, List<Book>>(message.Books);
            _bookShop.ReceiveDelivery(books);
            return Task.CompletedTask;
        }  
    }
}