namespace Task2
{
    public class Book
    {
        public int Id { get; }
        public string Genre { get; }
        public double Price { get; private set; }

        public bool Novelty { get; }

        public Book(int id, double price, string genre, bool novelty)
        {
            Id = id;
            Genre = genre;
            Price = price;
            Novelty = novelty;
        }

        public void RaisePrice(int percent)
        {
            Price = Price / (100 - percent) * 100;
        }

        public void ReducePrice(int percent)
        {
            Price -= Price / 100 * percent;
        }
    }
}