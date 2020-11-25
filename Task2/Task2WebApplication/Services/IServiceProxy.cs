using System.Collections.Generic;
using Task2;

namespace Task2WebApplication.Services
{
    public interface IServiceProxy
    {
        List<Book> GetData();
    }
}