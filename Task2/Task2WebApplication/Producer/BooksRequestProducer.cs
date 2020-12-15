using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Task2WebApplication.Producer
{
    public class BooksRequestProducer
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IConfiguration _configuration;

        public BooksRequestProducer(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _configuration = configuration;
        }

        public async Task SendBookRequest(int booksCount)
        {
            var message = new BooksRequest(booksCount);
            
            var hostConfig = new MassTransitConfiguration();
            _configuration.GetSection("MassTransit").Bind(hostConfig);
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(hostConfig.GetQueueAddress("book-shop-queue"));
            await endpoint.Send(message);
        }
    }
}