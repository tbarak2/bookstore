using BookStore.Interfaces;
using Microsoft.Extensions.Options;
using System;

namespace BookStore.Factories
{
    class RepositoryFactory : IRepoistoryFactory
    {
        private readonly ConnectionDriver _config;
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider, IOptions<ConnectionDriver> connectionDriver)
        {
            _config = connectionDriver?.Value ?? throw new ArgumentNullException(nameof(connectionDriver)); 
            _serviceProvider = serviceProvider;
        }

        public IBookRepository CreateBookRepository()
        {
            var connectionString = "BookStore.Repositroies." + _config.RepositoryType;
            return (IBookRepository)_serviceProvider.GetService(Type.GetType(connectionString));
        }
    }
}
