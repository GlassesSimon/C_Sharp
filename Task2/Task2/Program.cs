using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            BookShop myBookShop = new BookShop();
        }

        class BookShop
        {
            private readonly Library _shopLibrary = new Library();
            private readonly BankAccount _bankAccount = new BankAccount();
            private int capacity = 1000;

            public void BookSold(int id)
            {
                _shopLibrary.DeleteBook(id);
                _bankAccount.Add(_shopLibrary.Stock.Single(b => b.Id == id).Price);
                if (_shopLibrary.Stock.Count <= capacity / 10)
                {
                    DeliveryOrder();
                }

                int counter = 0;
                foreach (var book in _shopLibrary.Stock)
                {
                    if (!book.Novelty)
                    {
                        counter++;
                    }
                }

                if (counter / (_shopLibrary.Stock.Count / 100) >= 75)
                {
                    DeliveryOrder();
                }
            }

            public void Delivery(List<Book> delivery)
            {
                double counter = 0;
                foreach (var book in _shopLibrary.Stock)
                {
                    counter += book.Price / 100 * 7;
                }

                if (counter < _bankAccount.Balance)
                {
                    _bankAccount.Balance -= counter;

                    foreach (var book in delivery)
                    {
                        _shopLibrary.AddBook(book.Id);
                    }
                }
            }

            public void Sale(int percent)
            {
                foreach (var book in _shopLibrary.Stock)
                {
                    if (!book.Novelty)
                    {
                        book.ReducePrice(percent);
                    }
                }
            }

            public void SaleByGenre()
            {
                foreach (var book in _shopLibrary.Stock)
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

            public void CancelSale(int percent)
            {
                foreach (var book in _shopLibrary.Stock)
                {
                    if (!book.Novelty)
                    {
                        book.RaisePrice(percent);
                    }
                }
            }

            public void CancelSaleByGenre()
            {
                foreach (var book in _shopLibrary.Stock)
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

            public void DeliveryOrder()
            {
                Console.WriteLine("Need more books.");
            }
        }

        class Book
        {
            public int Id { get; }
            public string Genre { get; }
            public double Price { get; set; }

            public bool Novelty { get; }

            public Book(int id, double price, string genre, bool novelty)
            {
                Id = id;
                Genre = genre;
                Price = price;
                Novelty = novelty;
            }

            public void RaisePrice(int percent)
            {
                Price += Price / 100 * percent;
            }

            public void ReducePrice(int percent)
            {
                Price -= Price / 100 * percent;
            }
        }

        class Library
        {
            public List<Book> Stock = new List<Book>();

            public void DeleteBook(int id)
            {
                var book = Stock.FirstOrDefault(b => b.Id == id);
                if (book == null)
                {
                    throw new MyExeption("No book with this id");
                }

                Stock.Remove(book);
            }

            public void AddBook(int id)
            {
                var book = Stock.FirstOrDefault(b => b.Id == id);
                if (book == null)
                {
                    throw new MyExeption("No book with this id");
                }

                Stock.Add(book);
            }
        }

        class MyExeption : Exception
        {
            public override string Message { get; }

            public MyExeption(string message)
            {
                Message = message;
            }
        }

        class BankAccount
        {
            public double Balance { get; set; }

            public void Add(double amount)
            {
                Balance += amount;
            }

            public void Sub(double amount)
            {
                Balance -= amount;
            }
        }
    }
}