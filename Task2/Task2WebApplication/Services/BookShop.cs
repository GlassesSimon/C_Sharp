using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public readonly BankAccount ShopBankAccount = new BankAccount(StartingBalance);
        private int Capacity {get;}

        private readonly BankAccountContextDbContextFactory _bankAccountDdbContextFactory;
        private readonly BooksContextDbContextFactory _booksDbContextFactory;
        private readonly BooksRequestProducer _producer;
        
        public BookShop(int capacity)
        {
            Capacity = capacity;
        }
        
        public BookShop(BankAccountContextDbContextFactory bankAccountDdbContextFactory, BooksContextDbContextFactory booksDdbContextFactory, BooksRequestProducer producer)
        {
            _bankAccountDdbContextFactory = bankAccountDdbContextFactory;
            _booksDbContextFactory = booksDdbContextFactory;
            _producer = producer;
        }

        public void SellBook(int id) 
        {
            try
            {
                var book = ShopLibrary.Stock.FirstOrDefault(b => b.Id == id);
                if (book == null)
                {
                    throw new Exception("No book with this id.\n");
                }
                ShopBankAccount.Add(book.Price);
                ShopLibrary.DeleteBook(id);
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

        public void ReceiveDelivery(List<Book> delivery)
        {
            var counter = delivery.Sum(book => book.Price / 100 * 7);

            if (counter < ShopBankAccount.Balance)
            {
                try
                {
                    ShopBankAccount.Sub(counter);
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                }

                foreach (var book in delivery)
                {
                    ShopLibrary.AddBook(book);
                }
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
        
        private void UpdateShopInDataBase()
        {
            using var booksContext = _booksDbContextFactory.GetContext();
            booksContext.AddBooks(ShopLibrary.Stock);            
            using var bankAccountContext = _bankAccountDdbContextFactory.GetContext();
            bankAccountContext.AddBankAccount(ShopBankAccount);
        }
        
        public List<Book> GetBooks()
        {
            return new List<Book>(ShopLibrary.Stock);
        }
    }
}