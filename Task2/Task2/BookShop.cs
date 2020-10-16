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
            
            var counter = ShopLibrary.Stock.Count(book => !book.Novelty);

            if (ShopLibrary.Stock.Count == 0 || counter / ShopLibrary.Stock.Count * 100 >= 75)
            {
                DeliveryOrder();
            }
            else if (ShopLibrary.Stock.Count <= Capacity / 10)
            {
                DeliveryOrder();
            }
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
            foreach (var book in ShopLibrary.Stock.Where(book => !book.Novelty))
            {
                try
                {
                    switch (book.Genre)
                    {
                        case "fiction":
                            book.ReducePrice(3);
                            break;
                        case "adventures":
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
            foreach (var book in ShopLibrary.Stock.Where(book => !book.Novelty))
            {
                try
                {
                    switch (book.Genre)
                    {
                        case "fiction":
                            book.RaisePrice(3);
                            break;
                        case "adventures":
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

        private static void DeliveryOrder()
        {
            Console.Write("Need more books.\n");
        }
    }
}