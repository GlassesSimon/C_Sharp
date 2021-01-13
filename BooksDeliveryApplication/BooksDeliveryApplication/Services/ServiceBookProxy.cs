using System.Collections.Generic;
using System.Net.Http;
using BooksDeliveryApplication.Producer;
using ContractBookShop;
using Newtonsoft.Json;

namespace BooksDeliveryApplication.Services
{
    public class ServiceBookProxy: IServiceProxy
    {
        private readonly HttpClient _httpClient;
        private readonly BooksReceiveProducer _booksReceiveProducer;
        private const string Url = "https://getbooksrestapi.azurewebsites.net/api/books";

        public ServiceBookProxy(HttpClient httpClient, BooksReceiveProducer booksReceiveProducer)
        {
            _httpClient = httpClient;
            _booksReceiveProducer = booksReceiveProducer;
        }

        public async void GetAndSaveBooks(int booksCount)
        {
            var response = await _httpClient.GetAsync($"{Url}/{booksCount}");
            var json = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<IBooksResponse.Book>>(json);
            await _booksReceiveProducer.SendBooksToQueue(books);
        }
    }
}