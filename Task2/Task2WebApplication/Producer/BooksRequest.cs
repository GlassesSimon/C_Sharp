using ContractBookShop;

namespace Task2WebApplication.Producer
{
    public class BooksRequest: IBooksRequest
    {
        public int BooksCount { get; set; }

        public BooksRequest(int booksCount)
        {
            BooksCount = booksCount;
        }
    }
}