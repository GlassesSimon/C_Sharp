namespace Task2Infrastructure.EntityFramework
{
    public sealed class BooksContextDbContextFactory
    {
        private readonly string _connectionString;

        public BooksContextDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BooksContext GetContext()
        {
            return new BooksContext(BooksContextDesignTimeFactory.GetSqlServerOptions(_connectionString));
        }
    }
}