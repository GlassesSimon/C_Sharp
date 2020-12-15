using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Task2;
using Task2WebApplication.Services;

namespace Task2Test
{
    public class BookShopTest
    {
        [Test]
        public void SuccessfulDeliveryTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<Book> {new Book(1, "fiction", 350, false), new Book(2, "encyclopedia", 450, true)};
            myBookShop.ReceiveDelivery(delivery);
            
            myBookShop.ShopBankAccount.Balance.Should().Be(99944);
            myBookShop.ShopLibrary.Stock.Count.Should().Be(2);
        }        
        
        [Test]
        public void UnsuccessfulDeliveryTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<Book> {new Book(1, "fiction", 5000000, false)};
            myBookShop.ReceiveDelivery(delivery);
            myBookShop.ShopBankAccount.Balance.Should().Be(100000);
            myBookShop.ShopLibrary.Stock.Count.Should().Be(0);
        }

        [Test]
        public void NeedDeliveryTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<Book> {new Book(1, "fiction", 350, false), new Book(2, "encyclopedia", 450, true)};
            myBookShop.ReceiveDelivery(delivery);
            myBookShop.NeedDelivery().Should().Be(true);
        }        
        
        [Test]
        public void UnsuccessfulBookSoldTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<Book> {new Book(1, "fiction", 350, false), new Book(2, "encyclopedia", 450, true)};
            myBookShop.ReceiveDelivery(delivery);
            SellBookTest(myBookShop, 3, "No book with this id.\n");
        }
        
        [Test]
        public void SuccessfulBookSoldTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<Book> {new Book(1, "fiction", 350, false), new Book(2, "encyclopedia", 450, true), new Book(3, "fiction", 500, false)};
            myBookShop.ReceiveDelivery(delivery);
        }

        private static void SellBookTest(BookShop myBookShop, int id, string message)
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                myBookShop.SellBook(id);
                sw.ToString().Should().Be(message);
            }
        }

        [Test]
        public void SaleByGenreTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<Book> {new Book(1, "encyclopedia", 450, true), new Book(2, "fiction", 350, false), new Book(3, "adventure", 500, false), new Book(4, "encyclopedia", 450, false)};
            myBookShop.ReceiveDelivery(delivery);
            myBookShop.SaleByGenre();
            myBookShop.ShopLibrary.Stock[0].Price.Should().Be(450);
            myBookShop.ShopLibrary.Stock[1].Price.Should().Be(339.5);
            myBookShop.ShopLibrary.Stock[2].Price.Should().Be(465);
            myBookShop.ShopLibrary.Stock[3].Price.Should().Be(405);
        }        
        
        [Test]
        public void CancelSaleByGenreTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<Book> {new Book(1, "encyclopedia", 450, true), new Book(2, "fiction", 339.5, false), new Book(3, "adventure", 465, false), new Book(4, "encyclopedia", 405, false)};
            myBookShop.ReceiveDelivery(delivery);

            myBookShop.CancelSaleByGenre();
            myBookShop.ShopLibrary.Stock[0].Price.Should().Be(450);
            myBookShop.ShopLibrary.Stock[1].Price.Should().Be(350);
            myBookShop.ShopLibrary.Stock[2].Price.Should().Be(500);
            myBookShop.ShopLibrary.Stock[3].Price.Should().Be(450);
        }
    }
}