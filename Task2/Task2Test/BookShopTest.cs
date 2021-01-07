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
        public void UnsuccessfulDeliveryTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<Book> {new Book(1, "A", "fiction", 5000000, false, DateTime.Now)};
            myBookShop.ReceiveDelivery(delivery);
            myBookShop.ShopBankAccount.Balance.Should().Be(100000);
            myBookShop.ShopLibrary.Stock.Count.Should().Be(0);
        }

        [Test]
        public void NeedDeliveryTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<Book> {new Book(1, "A", "fiction", 350, false, DateTime.Now), new Book(2, "B", "encyclopedia", 450, true, DateTime.Now)};
            myBookShop.ReceiveDelivery(delivery);
            myBookShop.NeedDelivery().Should().Be(true);
        }        
        
        [Test]
        public void UnsuccessfulBookSoldTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<Book> {new Book(1, "A", "fiction", 350, false, DateTime.Now), new Book(2, "B","encyclopedia", 450, true, DateTime.Now)};
            myBookShop.ReceiveDelivery(delivery);
            SellBookTest(myBookShop, 3, "No book with this id.\n");
        }
        
        [Test]
        public void SuccessfulBookSoldTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<Book> {new Book(1,"A", "fiction", 350, false, DateTime.Now), new Book(2, "B", "encyclopedia", 450, true, DateTime.Now), new Book(3, "C", "fiction", 500, false, DateTime.Now)};
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
    }
}