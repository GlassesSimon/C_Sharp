using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Task2Infrastructure.EntityFramework
{
    [UsedImplicitly]
    public sealed class BankAccountContextDesignTimeFactory : IDesignTimeDbContextFactory<BankAccountContext>
    {
        private const string DefaultConnectionString =
            "Host=localhost;Username=postgres;Password=NSU;Database=BookShop;";

        public static DbContextOptions<BankAccountContext> GetSqlServerOptions([CanBeNull] string connectionString)
        {
            return new DbContextOptionsBuilder<BankAccountContext>()
                .UseNpgsql(connectionString ?? DefaultConnectionString,
                    x => { x.MigrationsHistoryTable("__EFMigrationsHistory", BankAccountContext.DefaultSchemaName); })
                .Options;
        }

        public BankAccountContext CreateDbContext(string[] args)
        {
            return new BankAccountContext(GetSqlServerOptions(null));
        }
    }
}