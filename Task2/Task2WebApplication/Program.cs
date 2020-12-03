using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Task2WebApplication
{
    public class Program
    {
        #warning папку idea тоже лучше в gitignore засунуть http://prntscr.com/vv2ghl
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}