using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task2;

namespace Task2Infrastructure.EntityFramework.Configurations
{
    [UsedImplicitly]
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(nameof(Book), BooksContext.DefaultSchemaName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Genre).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Novelty).IsRequired();

        }
    }
}