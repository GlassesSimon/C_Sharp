using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Task2;

namespace Task2Test
{
    #warning Ангелина, очень плохие названия тестов) название теста должно отражать суть, что в тесте проверяется 
    public class BookShopTest
    {
        [Test]
        public void Test1()
        {
            var myBookShop = new BookShop(BookShop.StartingCapacity);
            #warning создаёшь инстанс Library, а переменная называется delivery. Непонятно :) 
            #warning честно говоря, не понимаю, зачем ты создаёшь тут инстанс Library, чтобы туда добавить книг и потом их передать в delivery
            #warning можно же просто создать List<Book> и его передать в delivery
            var delivery = new Library();
            delivery.AddBook(new Book(1, 350, "fiction", false));
            delivery.AddBook(new Book(2, 450, "encyclopedia", true));
            myBookShop.Delivery(delivery.Stock);
            
            myBookShop.ShopBankAccount.Balance.Should().Be(99944);
            myBookShop.ShopLibrary.Stock.Count.Should().Be(2);

            #warning вот добавление следующих строчек ведёт к тому, что этот тест надо разбивать на 2, т.к. у тебя рассматривается 2 кейса: 
            #warning первый - приём доставки добавляет книги в библиотеку и вычитает сумму за доставку
            #warning а вот этот уже второй: если у нас нет денег, чтобы принять доставку, то библиотека магазина остаётся прежней
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
            
            #warning все обычно стараются бороться с копипастой :) можно вынести эту проверку в метод, который будет принимать myBookShop и id книги для продажи
            #warning ну и назвать метод красиво
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