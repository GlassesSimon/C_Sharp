namespace Task2
{
    public class Book
    {
        public int Id { get; }
        public string Genre { get; }
        public double Price { get; private set; }

        public bool Novelty { get; }

        #warning такой конструктор имеет место быть (хоть количество аргументов уже и подходит к предельному; 
        #warning я бы скорее не делал такой конструктор, а использовал конструктор по умолчанию; 
        public Book(int id, string genre, double price, bool novelty)
        {
            Id = id;
            Genre = genre;
            Price = price;
            Novelty = novelty;
        }

        public Book(int id, double price, string genre, bool novelty)
        {
            Id = id;
            Genre = genre;
            Price = price;
            Novelty = novelty;
        }

        public void RaisePrice(int percent)
        {
            #warning на percent есть какие-то ограничения? скидка, наверное, не может быть меньше 0% и больше 100%
            Price = Price / (100 - percent) * 100;
        }

        public void ReducePrice(int percent)
        {
            #warning ну и тут аналогично
            Price -= Price / 100 * percent;
        }
    }
}