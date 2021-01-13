using System;
using JetBrains.Annotations;

namespace Task2
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class BankAccount
    {
        public int Id { get; private set; }
        public double Balance { get; private set; }

        public BankAccount(double balance)
        {
            Balance = balance;
        }        
        
        public BankAccount(int id, double balance)
        {
            Id = id;
            Balance = balance;
        }

        public void Add(double amount)
        {
            Balance += amount;
        }
        
        public void Sub(double amount)
        {
            if (Balance < amount)
            {
                throw new Exception("Not enough money.\n");
            } 
            
            Balance -= amount;
        }
    }
}