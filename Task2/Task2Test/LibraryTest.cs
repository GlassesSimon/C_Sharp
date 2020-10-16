using System;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Task2;

namespace Task2Test
{
    public class LibraryTest
    {
        [Test]
        public void AddBookTest()
        {
            var myLibrary = new Library();
            myLibrary.AddBook(new Book(1, "fiction", 350,true));
            myLibrary.Stock.Count.Should().Be(1);
            myLibrary.Stock[0].Id.Should().Be(1);
            myLibrary.Stock[0].Price.Should().Be(350);
            myLibrary.Stock[0].Genre.Should().Be("fiction");
            myLibrary.Stock[0].Novelty.Should().Be(true);
        }
        
        [Test]
        public void DeleteBookTest()
        {
            var myLibrary = new Library();
            myLibrary.AddBook(new Book(1, "fiction", 350,true));
            myLibrary.DeleteBook(1);
            myLibrary.Stock.Count.Should().Be(0);
            myLibrary.Stock.FirstOrDefault(b => b.Id == 1).Should().Be(null);

            Action act = () => myLibrary.DeleteBook(2);
            act.Should().Throw<Exception>().WithMessage("No book with this id.\n");
        }
    }
}