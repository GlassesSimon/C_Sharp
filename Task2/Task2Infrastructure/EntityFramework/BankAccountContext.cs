using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task2;

namespace Task2Infrastructure.EntityFramework
{
    public class BankAccountContext: DbContext
    {
        public const string DefaultSchemaName = "public";

        public BankAccountContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.HasDefaultSchema(DefaultSchemaName);
        }

        public async Task<BankAccount> GetBankAccount()
        {
            return await Set<BankAccount>()
                .FirstOrDefaultAsync();
        }

        public void AddBankAccount(BankAccount bankAccount)
        {
            Set<BankAccount>().Add(bankAccount);
        }
    }
}