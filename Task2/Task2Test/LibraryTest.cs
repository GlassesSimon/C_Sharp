using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Task2;

namespace Task2Test
{
    public class LibraryTest
    {
        [Test]
        public void AddBookTest()
        {
            var myLibrary = new Library();
            myLibrary.AddBooks(new List<Book> {new Book(1, "fiction", 350, true)});
            myLibrary.Stock.Count.Should().Be(1);
            myLibrary.Stock[0].Id.Should().Be(1);
            myLibrary.Stock[0].Price.Should().Be(350);
            myLibrary.Stock[0].Genre.Should().Be("fiction");
            myLibrary.Stock[0].IsNew.Should().Be(true);
        }
        
        [Test]
        public void SuccessfulDeleteBookTest()
        {
            var myLibrary = new Library();
            myLibrary.AddBooks(new List<Book> {new Book(1, "fiction", 350,true)});
            myLibrary.DeleteBook(1);
            myLibrary.Stock.Count.Should().Be(0);
            myLibrary.Stock.FirstOrDefault(b => b.Id == 1).Should().Be(null);
        }        

        [Test]
        public void ShouldNotDeleteTheBookIfThereIsNoBookWithThisId()
        {
            var myLibrary = new Library();

            Action act = () => myLibrary.DeleteBook(1);
            act.Should().Throw<Exception>().WithMessage("No book with this id.\n");
        }
    }
}