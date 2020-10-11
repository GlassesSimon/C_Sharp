using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    public class Library
    {
        public readonly List<Book> Stock = new List<Book>();

        public void DeleteBook(int id)
        {
            var book = Stock.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new MyException("No book with this id.");
            }

            Stock.Remove(book);
        }

        public void AddBook(Book book)
        {
            Stock.Add(book);
        }
    }
}