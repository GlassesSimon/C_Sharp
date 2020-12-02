using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Task2;

namespace Task2WebApplication.Services
{
    public class ServiceProxy: IServiceProxy
    {
        private readonly BookShop _bookShop;
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://getbooksrestapi.azurewebsites.net/api/books";

        public ServiceProxy(HttpClient httpClient, BookShop bookShop)
        {
            _httpClient = httpClient;
            _bookShop = bookShop;
        }

        public async void GetAndSaveBooks()
        {
            if (!_bookShop.NeedDelivery())
            {
                return;
            }
            var response = await _httpClient.GetAsync(_url + 10);
            var books = JsonConvert.DeserializeObject<List<Book>>(response.Content.ToString());
            _bookShop.ReceiveDelivery(books);
        }
    }
}