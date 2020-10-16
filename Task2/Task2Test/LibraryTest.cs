using System;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Task2;

namespace Task2Test
{
    public class LibraryTest
    {
        #warning название метода
        [Test]
        public void Tests()
        {
            var myLibrary = new Library();
            myLibrary.AddBook(new Book(1, 350, "fiction", true));
            myLibrary.Stock.Count.Should().Be(1);
            myLibrary.Stock[0].Id.Should().Be(1);
            myLibrary.Stock[0].Price.Should().Be(350);
            myLibrary.Stock[0].Genre.Should().Be("fiction");
            myLibrary.Stock[0].Novelty.Should().Be(true);
            
            myLibrary.DeleteBook(1);
            myLibrary.Stock.Count.Should().Be(0);
            myLibrary.Stock.FirstOrDefault(b => b.Id == 1).Should().Be(null);

                /*
                 try/catch в тестах не надо писать; лучше написать вот. сегодня на лекции будет, что за Action такой
            Action act = () => myLibrary.DeleteBook(2);
            act.Should().Throw<MyException>();*/

            try
            {
                myLibrary.DeleteBook(2);
            }
            catch (MyException exception)
            {
                exception.Message.Should().Be("No book with this id.");
            }
        }
    }
}