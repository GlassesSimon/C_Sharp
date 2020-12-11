using System;
using FluentAssertions;
using NUnit.Framework;
using Task2;

namespace Task2Test
{
    public class BookTest
    {
        [Test]
        public void ReducePriceTest()
        {
            var myBook = new Book(1, "fiction", 350, true);
            myBook.ReducePrice(10);
            myBook.Price.Should().Be(315);
            
            Action act1 = () => myBook.ReducePrice(110);
            act1.Should().Throw<Exception>().WithMessage("Percentage is incorrect.\n");            
            
            Action act2 = () => myBook.ReducePrice(-10);
            act2.Should().Throw<Exception>().WithMessage("Percentage is incorrect.\n");
        }            
        
        [Test]
        public void RaisePriceTest()
        {
            var myBook = new Book(1, "fiction", 315, true);
            myBook.RaisePrice(10);
            myBook.Price.Should().Be(350); 
            
            Action act1 = () => myBook.RaisePrice(110);
            act1.Should().Throw<Exception>().WithMessage("Percentage is incorrect.\n");            
            
            Action act2 = () => myBook.RaisePrice(-10);
            act2.Should().Throw<Exception>().WithMessage("Percentage is incorrect.\n");
        }       
    }
}