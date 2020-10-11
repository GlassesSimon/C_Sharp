namespace Task2
{
    public class BankAccount
    {
        public double Balance { get; private set; }

        public BankAccount(double balance)
        {
            Balance = balance;
        }

        public void Add(double amount)
        {
            Balance += amount;
        }

        public void Sub(double amount)
        {
            Balance -= amount;
        }
    }
}