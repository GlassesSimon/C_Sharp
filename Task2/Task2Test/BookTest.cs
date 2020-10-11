using NUnit.Framework;
using FluentAssertions;
using Task2;

namespace Task2Test
{
    public class BookTest
    {
        [Test]
        public void Tests()
        {
            var myBook = new Book(1, 350, "fiction", true);
            myBook.ReducePrice(10);
            myBook.Price.Should().Be(315);
            
            myBook.RaisePrice(10);
            myBook.Price.Should().Be(350); 
        }       
    }
}