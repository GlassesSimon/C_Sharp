using NUnit.Framework;
using FluentAssertions;
using Task2;

namespace Task2Test
{
    public class BankAccountTest
    {
#warning название метода, можно даже разделить на два теста
        [Test]
        public void Tests()
        {
            var myBankAccount = new BankAccount(BookShop.StartingBalance);
            myBankAccount.Add(300);
            myBankAccount.Balance.Should().Be(100300);
            
            myBankAccount.Sub(10300);
            myBankAccount.Balance.Should().Be(90000);
        }
    }
}