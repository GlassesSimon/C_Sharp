using System;
using JetBrains.Annotations;

namespace Task2
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Book
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public double Price { get; private set; }
        public bool IsNew { get; private set; }
        public DateTime DateDelivery { get; private set; }

        public Book(int id, string title, string genre, double price, bool isNew, DateTime dateDelivery)
        {
            Id = id;
            Title = title;
            Genre = genre;
            Price = price;
            IsNew = isNew;
            DateDelivery = dateDelivery;
        }
        
        public void RaisePrice(int percent)
        {
            if (percent > 100 || percent < 0)
            {
                throw new Exception("Percentage is incorrect.\n");
            }
            Price = Price / (100 - percent) * 100;
        }

        public void ReducePrice(int percent)
        {
            if (percent > 100 || percent < 0)
            {
                throw new Exception("Percentage is incorrect.\n");
            }
            Price -= Price / 100 * percent;
        }
    }
}