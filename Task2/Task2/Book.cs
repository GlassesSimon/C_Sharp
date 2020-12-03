using System;

namespace Task2
{
    public class Book
    {
        public int Id { get; }
        public string Genre { get; }
        public double Price { get; private set; }
        public bool Novelty { get; private set; }

        public Book(int id, string genre, double price, bool novelty)
        {
            Id = id;
            Genre = genre;
            Price = price;
            Novelty = novelty;
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