using BookStore.Model;
using System.Collections.Generic;

namespace BookStore.Interfaces
{
    interface IListLoader
    {
        public List<Book> GetData();

        public void LoadData();
    }
}
