using System;
using System.Collections.Generic;
using System.IO;
using ContractBookShop;
using FluentAssertions;
using NUnit.Framework;
using Task2WebApplication.Services;

namespace Task2Test
{
    public class BookShopTest
    {
        [Test]
        public void UnsuccessfulDeliveryTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<IBooksResponse.Book> {new IBooksResponse.Book{ Id = 1, Title = "A", Genre = "fiction", Price = 5000000, IsNew = false, DateDelivery = DateTime.Now}};
            myBookShop.ReceiveDelivery(delivery);
            myBookShop.ShopBankAccount.Balance.Should().Be(100000);
            myBookShop.ShopLibrary.Stock.Count.Should().Be(0);
        }

        [Test]
        public void NeedDeliveryTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<IBooksResponse.Book> {new IBooksResponse.Book{ Id = 1, Title = "A", Genre = "fiction", Price = 5000000, IsNew = false, DateDelivery = DateTime.Now}, 
                new IBooksResponse.Book{ Id = 2, Title = "B", Genre = "encyclopedia", Price = 450, IsNew = true, DateDelivery = DateTime.Now}};
            myBookShop.ReceiveDelivery(delivery);
            myBookShop.NeedDelivery().Should().Be(true);
        }        
        
        [Test]
        public void UnsuccessfulBookSoldTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<IBooksResponse.Book> {new IBooksResponse.Book{ Id = 1, Title = "A", Genre = "fiction", Price = 350, IsNew = false, DateDelivery = DateTime.Now}, 
                new IBooksResponse.Book{ Id = 2, Title = "B", Genre = "encyclopedia", Price = 450, IsNew = true, DateDelivery = DateTime.Now}};
            myBookShop.ReceiveDelivery(delivery);
            SellBookTest(myBookShop, 3, "No book with this id.\n");
        }
        
        [Test]
        public void SuccessfulBookSoldTest()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            var delivery = new List<IBooksResponse.Book> {new IBooksResponse.Book{ Id = 1, Title = "A", Genre = "fiction", Price = 350, IsNew = false, DateDelivery = DateTime.Now}, 
                new IBooksResponse.Book{ Id = 2, Title = "B", Genre = "encyclopedia", Price = 450, IsNew = true, DateDelivery = DateTime.Now}, 
                new IBooksResponse.Book{ Id = 3, Title = "C", Genre = "fiction", Price = 500, IsNew = false, DateDelivery = DateTime.Now}};
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