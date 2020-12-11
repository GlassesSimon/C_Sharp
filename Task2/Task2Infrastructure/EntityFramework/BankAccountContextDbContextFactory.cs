namespace Task2Infrastructure.EntityFramework
{
    public class BankAccountContextDbContextFactory
    {
        private readonly string _connectionString;

        public BankAccountContextDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BankAccountContext GetContext()
        {
            return new BankAccountContext(BankAccountContextDesignTimeFactory.GetSqlServerOptions(_connectionString));
        }
    }
}