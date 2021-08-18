using BookStore.Interfaces;
using BookStore.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Connections
{
    class ListLoaderService : IListLoaderService
    {
        private readonly IListLoaderFactory _listLoaderFactory;
        private IListLoader _listLoader;
        private readonly IConfiguration _configuraion;


        public ListLoaderService(IListLoaderFactory listLoaderFactory,  IConfiguration configuraion)
        {
            _configuraion = configuraion;
            _listLoaderFactory = listLoaderFactory;

        }

        private IListLoader GetListLoader()
        {
            return _listLoaderFactory.CreateListLoader();
        }

        public List<Book> GetData()
        {
            if (_listLoader == null)
                _listLoader = GetListLoader();
            return _listLoader.GetData();
        }

        public void LoadData()
        {
            if (_listLoader == null)
                _listLoader = GetListLoader();
            _listLoader.LoadData();
        }
    }
}
