using BookStore.Connections;
using BookStore.Factories;
using BookStore.Interfaces;
using BookStore.ListLoaders;
using BookStore.Model;
using BookStore.Repositroies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace BookStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
               
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            //var service = serviceProvider.GetService<IDaoService>();
            var listService = serviceProvider.GetService<IListLoaderService>();
            listService.LoadData();
            //service.Connect();
            //service.Insert(new Book
            //{
            //    Author = "James",
            //    Description = "Some love story in the future",
            //    Genre = "Scfi",
            //    Id = "Md1",
            //    Price = 2,
            //    PublishDate = DateTime.Now,
            //    Title = "The Best Book Ever"
            //});
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables();
            IConfiguration Configuration = configuration.Build();
           // var S = configuration.GetSection("ConnectionStrings");
            services.Configure<ConnectionDriver>(Configuration.GetSection("ConnectionStrings"));
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddTransient<IRepoistoryFactory, RepositoryFactory>()
                .AddTransient<IDaoService, DaoService>()
                .AddScoped<CsvBookRepository>()
                .AddScoped<IBookRepository, CsvBookRepository>(r => r.GetService<CsvBookRepository>())
                .AddTransient<IListLoaderFactory, ListLoaderFactory>()
                .AddTransient<IListLoaderService, ListLoaderService>()
                .AddScoped<JsonListLoader>()
                .AddScoped<IListLoader, JsonListLoader>(ll => ll.GetService<JsonListLoader>());
        }

        //public Startup(IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //        .AddEnvironmentVariables();

        //   // Microsoft.Extensions.Configuration = builder.Build();
        //}
    }
}
