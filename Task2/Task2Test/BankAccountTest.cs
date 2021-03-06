﻿using System;
using FluentAssertions;
using NUnit.Framework;
using Task2;
using Task2WebApplication.Services;

namespace Task2Test
{
    public class BankAccountTest
    {
        [Test]
        public void AddTest()
        {
            var myBankAccount = new BankAccount(BookShop.StartingBalance);
            myBankAccount.Add(300);
            myBankAccount.Balance.Should().Be(100300);
        }        
        
        [Test]
        public void SubTest()
        {
            var myBankAccount = new BankAccount(BookShop.StartingBalance);
            
            myBankAccount.Sub(10000);
            myBankAccount.Balance.Should().Be(90000);
       
            Action act = () => myBankAccount.Sub(100000);
            act.Should().Throw<Exception>().WithMessage("Not enough money.\n");
        }
    }
}