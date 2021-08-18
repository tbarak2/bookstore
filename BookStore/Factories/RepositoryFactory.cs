using BookStore.Interfaces;
using BookStore.Model;
using BookStore.Repositroies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var connectionString = "BookStore.Repositroies." + _config.RepositoryType;//+_config.GetValue<string>("ConnectionStrings:RepositoryType");
            return (IBookRepository)_serviceProvider.GetService(Type.GetType(connectionString));
        }
    }
}
