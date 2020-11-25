using System.Collections.Generic;
using System.Net.Http;
using Task2;

namespace Task2WebApplication.Services
{
    public class ServiceProxy: IServiceProxy
    {
        private readonly HttpClient _httpClient;

        public ServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Book> GetData()
        {
            throw new System.NotImplementedException();
        }
    }
}