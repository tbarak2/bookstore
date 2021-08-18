using BookStore.Interfaces;
using BookStore.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Connections
{
    class DaoService : IDaoService
    {
        private readonly IRepoistoryFactory _repoistoryFactory;
        private IBookRepository _bookRepository;
        private readonly IOptions<ConnectionDriver> _config;
        private readonly IConfiguration _configuraion;


        public DaoService(IRepoistoryFactory repoistoryFactory, IOptions<ConnectionDriver> connectionDriver,IConfiguration configuraion)
        {
            _configuraion = configuraion;
            _config = connectionDriver;
            _repoistoryFactory = repoistoryFactory;
            
        }
        public void Connect()
        {
            _bookRepository = GetBookRepository();
            _bookRepository.Connect();
        }

        private IBookRepository GetBookRepository()
        {
            return _repoistoryFactory.CreateBookRepository();
        }

        public int Insert(Book book)
        {
            return _bookRepository.Insert(book);

        }
    }
}
