using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{ 
    public class BookShop
    {
        public const double StartingBalance = 100000;
        public const int StartingCapacity = 100000;
        
        public readonly Library ShopLibrary = new Library(); 
        public readonly BankAccount ShopBankAccount = new BankAccount(StartingBalance);
        private int Capacity {get;}

        public BookShop(int capacity)
        {
            Capacity = capacity;
        }

        public void BookSold(int id) 
        {
            try
            {
                var book = ShopLibrary.Stock.FirstOrDefault(b => b.Id == id);
                if (book == null)
                {
                    throw new MyException("No book with this id.");
                }
                ShopBankAccount.Add(book.Price);
                ShopLibrary.DeleteBook(id);
            }
            catch (MyException exception)
            {
                Console.WriteLine(exception.Message);
            }
            
            var counter = 0;
            foreach (var book in ShopLibrary.Stock)
            {
                if (!book.Novelty)
                {
                    counter++;
                }
            }

            if (ShopLibrary.Stock.Count == 0 || counter / ShopLibrary.Stock.Count * 100 >= 75)
            {
                DeliveryOrder();
            }
            else if (ShopLibrary.Stock.Count <= Capacity / 10)
            {
                DeliveryOrder();
            }
        }

        public void Delivery(List<Book> delivery)
        {
            double counter = 0;
            foreach (var book in delivery)
            {
                counter += book.Price / 100 * 7;
            }

            if (counter < ShopBankAccount.Balance)
            {
                ShopBankAccount.Sub(counter);

                foreach (var book in delivery)
                {
                    ShopLibrary.AddBook(book);
                }
            }
        }

        public void SaleByGenre()
        {
            foreach (var book in ShopLibrary.Stock)
            {
                if (!book.Novelty)
                {
                    if (book.Genre == "fiction")
                    {
                        book.ReducePrice(3);
                    }
                    else if (book.Genre == "adventures")
                    {
                        book.ReducePrice(7);
                    }
                    else if (book.Genre == "encyclopedia")
                    {
                        book.ReducePrice(10);
                    }
                }
            }
        }
        
        public void CancelSaleByGenre()
        {
            foreach (var book in ShopLibrary.Stock)
            {
                if (!book.Novelty)
                {
                    if (book.Genre == "fiction")
                    {
                        book.RaisePrice(3);
                    }
                    else if (book.Genre == "adventures")
                    {
                        book.RaisePrice(7);
                    }
                    else if (book.Genre == "encyclopedia")
                    {
                        book.RaisePrice(10);
                    }
                }
            }
        }

        private static void DeliveryOrder()
        {
            Console.Write("Need more books.\n");
        }
    }
}