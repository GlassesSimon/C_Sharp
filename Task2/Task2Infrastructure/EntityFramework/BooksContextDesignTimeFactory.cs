using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Task2Infrastructure.EntityFramework
{
    [UsedImplicitly]
    public sealed class BooksContextDesignTimeFactory : IDesignTimeDbContextFactory<BooksContext>
    {
        #warning зачем\почему этот закоменченый код тут? закоменченый код засоряет код, а иногда вызывает вопросы, зачем он тут. 
        //private const string DefaultConnectionString = "Data Source=127.0.0.1;Initial Catalog=BookShop;User Id=postgres; Password=NSU;";
        private const string DefaultConnectionString = "Host=localhost;Username=postgres;Password=NSU;Database=BookShop;";
        public static DbContextOptions<BooksContext> GetSqlServerOptions([CanBeNull]string connectionString)
        {
            return new DbContextOptionsBuilder<BooksContext>()
                .UseNpgsql(connectionString ?? DefaultConnectionString, x =>
                {
                    x.MigrationsHistoryTable("__EFMigrationsHistory", BooksContext.DefaultSchemaName);
                })
                .Options;
        }
        public BooksContext CreateDbContext(string[] args)
        {
            return new BooksContext(GetSqlServerOptions(null));
        }
    }
}
