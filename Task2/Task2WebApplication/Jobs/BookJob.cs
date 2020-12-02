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
        private readonly IServiceProxy _serviceProxy;

        public BookJob(IServiceProxy dataService)
        {
            _serviceProxy = dataService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _serviceProxy.GetAndSaveBooks();
            return Task.CompletedTask;
        }
    }
}