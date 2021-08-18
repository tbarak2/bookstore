using BookStore.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace BookStore.Factories
{
    class ListLoaderFactory : IListLoaderFactory
    {
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;

        public ListLoaderFactory(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _config = configuration;
            _serviceProvider = serviceProvider;
        }
        public IListLoader CreateListLoader()
        {
            var loader = "BookStore.ListLoaders." + _config["ListLoader:Driver"];
            return (IListLoader)_serviceProvider.GetService(Type.GetType(loader));
        }
    }
}
