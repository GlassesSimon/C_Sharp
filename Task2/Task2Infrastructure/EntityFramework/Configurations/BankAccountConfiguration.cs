﻿using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task2;

namespace Task2Infrastructure.EntityFramework.Configurations
{
    [UsedImplicitly]
    public class BankAccountConfiguration: IEntityTypeConfiguration<BankAccount>
    {
        private const double StartingBalance = 100000;

        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable(nameof(BankAccount), BankAccountContext.DefaultSchemaName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            
            builder.Property(x => x.Balance).IsRequired();

            builder.HasData(new BankAccount(1, StartingBalance));
        }
    }
}