﻿using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Task2Infrastructure.EntityFramework;
using Task2WebApplication.Services;

namespace Task2WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
#warning ни в appsettings.json, ни в appsettings.Development.json, ни в launchSettings.json у тебя нет секции со строкой подключения
#warning да, из-за этого у тебя в BooksContextTimeFactory просто берётся дефолтная строка, которая смотрит на локальную базу 
#warning но в случае деплоя на какой-нибудь сервер у тебя явно должен быть прописан ConnectionString до какой-то нормальной базы, не локальной
#warning не всегда (почти никогда) у тебя не будет базы на том же сервере, на котором запускается приложение
            services.AddSingleton(isp => new BooksContextDbContextFactory(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IServiceProxy, ServiceProxy>();
            
            #warning нет работы с rabbit mq, нет второго приложения
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}