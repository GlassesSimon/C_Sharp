using System.Threading.Tasks;
using JetBrains.Annotations;
using Quartz;
using Task2WebApplication.Services;

namespace Task2WebApplication.Jobs
{
    [UsedImplicitly]
    [DisallowConcurrentExecution]
    public class BookJob  : IJob
    {
        private readonly BookShop _bookShop;

        public BookJob(BookShop bookShop)
        {
            _bookShop = bookShop;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (_bookShop.NeedDelivery())
            {
                await _bookShop.DeliveryOrder(5);
            }
        }
    }
}