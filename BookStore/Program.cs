using BookStore.Connections;
using BookStore.Factories;
using BookStore.Interfaces;
using BookStore.ListLoaders;
using BookStore.Repositroies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            var listService = serviceProvider.GetService<IListLoaderService>();

            try
            {
                listService.LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables();
            IConfiguration Configuration = configuration.Build();
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
    }
}
