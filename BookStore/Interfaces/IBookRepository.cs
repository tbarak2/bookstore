using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Interfaces
{
    interface IBookRepository
    {
        void Connect();
        int Insert(Book book);
        
    }
}
