using BookStore.Model;
using System.Collections.Generic;

namespace BookStore.Interfaces
{
    interface IListLoaderService
    {
        public List<Book> GetData();

        public void LoadData();
    }
}
