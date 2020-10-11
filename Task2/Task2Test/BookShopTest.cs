using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Task2;

namespace Task2Test
{
    public class BookShopTest
    {
        [Test]
        public void Test1()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new Library();
            delivery.AddBook(new Book(1, 350, "fiction", false));
            delivery.AddBook(new Book(2, 450, "encyclopedia", true));
            myBookShop.Delivery(delivery.Stock);
            myBookShop.ShopBankAccount.Balance.Should().Be(99944);
            myBookShop.ShopLibrary.Stock.Count.Should().Be(2);
            
            delivery.AddBook(new Book(3, 5000000, "adventures", true));
            myBookShop.Delivery(delivery.Stock);
            myBookShop.ShopBankAccount.Balance.Should().Be(99944);
            myBookShop.ShopLibrary.Stock.Count.Should().Be(2);
        }

        [Test]
        public void Test2()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new Library();
            delivery.AddBook(new Book(1, 350, "fiction", false));
            delivery.AddBook(new Book(2, 450, "encyclopedia", true));
            myBookShop.Delivery(delivery.Stock);
            
            try
            {
                myBookShop.BookSold(3);
            }
            catch (MyException exception)
            {
                exception.Message.Should().Be("No book with this id.");
            }
            
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                myBookShop.BookSold(2);
                sw.ToString().Should().Be("Need more books.\n");
            }
            myBookShop.ShopLibrary.Stock.FirstOrDefault(b => b.Id == 2).Should().Be(null);
            myBookShop.ShopBankAccount.Balance.Should().Be(100394);
            
            delivery.DeleteBook(1);
            myBookShop.Delivery(delivery.Stock);
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                myBookShop.BookSold(1);
                sw.ToString().Should().Be("Need more books.\n");
            }
        }
        
        [Test]
        public void Test3()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new Library();
            delivery.AddBook(new Book(2, 450, "encyclopedia", true));
            delivery.AddBook(new Book(1, 350, "fiction", false));
            delivery.AddBook(new Book(3, 500, "adventures", false));
            delivery.AddBook(new Book(4, 450, "encyclopedia", false));
            myBookShop.Delivery(delivery.Stock);
            myBookShop.SaleByGenre();
            myBookShop.ShopLibrary.Stock[0].Price.Should().Be(450);
            myBookShop.ShopLibrary.Stock[1].Price.Should().Be(339.5);
            myBookShop.ShopLibrary.Stock[2].Price.Should().Be(465);
            myBookShop.ShopLibrary.Stock[3].Price.Should().Be(405);
            
            myBookShop.CancelSaleByGenre();
            myBookShop.ShopLibrary.Stock[0].Price.Should().Be(450);
            myBookShop.ShopLibrary.Stock[1].Price.Should().Be(350);
            myBookShop.ShopLibrary.Stock[2].Price.Should().Be(500);
            myBookShop.ShopLibrary.Stock[3].Price.Should().Be(450);
        }
    }
}