using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContractBookShop;
using Task2;
using Task2Infrastructure.EntityFramework;
using Task2WebApplication.Producer;

namespace Task2WebApplication.Services
{
    public class BookShop
    {
        public const double StartingBalance = 100000;
        public const int StartingCapacity = 100000;

        public readonly Library ShopLibrary = new Library();
        public readonly BankAccount ShopBankAccount;
        private int Capacity { get; } = StartingCapacity;

        private readonly BankAccountContextDbContextFactory _bankAccountDdbContextFactory;
        private readonly BooksContextDbContextFactory _booksDbContextFactory;
        private readonly BooksRequestProducer _producer;

        public BookShop(int capacity)
        {
            Capacity = capacity;
            ShopBankAccount = new BankAccount(StartingBalance);
        }

        public BookShop(BankAccountContextDbContextFactory bankAccountDdbContextFactory,
            BooksContextDbContextFactory booksDdbContextFactory, BooksRequestProducer producer)
        {
            _bankAccountDdbContextFactory = bankAccountDdbContextFactory;
            _booksDbContextFactory = booksDdbContextFactory;
            _producer = producer;
            
            using var context = _booksDbContextFactory.GetContext();
            ShopLibrary.AddBooks(context.GetBooks().Result);
        }

        public async Task SellBook(int id)
        {
            try
            {
                var book = ShopLibrary.Stock.FirstOrDefault(b => b.Id == id);
                if (book == null)
                {
                    throw new Exception("No book with this id.\n");
                }

                ShopLibrary.DeleteBook(id);
                
                await using var context = _booksDbContextFactory.GetContext();
                context.DeleteBook(book);
                await context.SaveChangesAsync();
                
                await using var contextBank = _bankAccountDdbContextFactory.GetContext();
                var account = await contextBank.GetBankAccount();
                account.Add(book.Price);
                await contextBank.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
        }

        public bool NeedDelivery()
        {
            var counter = ShopLibrary.Stock.Count(book => !book.IsNew);

            if (ShopLibrary.Stock.Count == 0 || counter / ShopLibrary.Stock.Count * 100 >= 75)
            {
                return true;
            }

            return ShopLibrary.Stock.Count <= Capacity / 10;
        }

        public async Task ReceiveDelivery(List<IBooksResponse.Book> booksFromMessage)
        {
            var delivery = booksFromMessage.Select(book => new Book(0, book.Title, book.Genre, book.Price, book.IsNew, book.DateDelivery)).ToList();

            var deliveryCost = delivery.Sum(book => book.Price / 100 * 7);

            await using var contextBank = _bankAccountDdbContextFactory.GetContext();
            var account = await contextBank.GetBankAccount();
            if (deliveryCost < account.Balance && Capacity >= delivery.Count + ShopLibrary.Stock.Count)
            {
                try
                {
                    account.Sub(deliveryCost);
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                }

                ShopLibrary.AddBooks(delivery);
                
                await using var context = _booksDbContextFactory.GetContext();
                context.AddBooks(delivery);
                await context.SaveChangesAsync();
                
                account.Sub(deliveryCost);
                await contextBank.SaveChangesAsync();
            }
        }

        public void SaleByGenre()
        {
            foreach (var book in ShopLibrary.Stock.Where(book => !book.IsNew))
            {
                try
                {
                    switch (book.Genre)
                    {
                        case "fiction":
                            book.ReducePrice(3);
                            break;
                        case "adventure":
                            book.ReducePrice(7);
                            break;
                        case "encyclopedia":
                            book.ReducePrice(10);
                            break;
                    }
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                }
            }
        }

        public void CancelSaleByGenre()
        {
            foreach (var book in ShopLibrary.Stock.Where(book => !book.IsNew))
            {
                try
                {
                    switch (book.Genre)
                    {
                        case "fiction":
                            book.RaisePrice(3);
                            break;
                        case "adventure":
                            book.RaisePrice(7);
                            break;
                        case "encyclopedia":
                            book.RaisePrice(10);
                            break;
                    }
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                }
            }
        }

        public async Task DeliveryOrder(int count)
        {
            await _producer.SendBookRequest(count);
            Console.Write("Need more books.\n");
        }

        public List<Book> GetBooks()
        {
            return new List<Book>(ShopLibrary.Stock);
        }
    }
}