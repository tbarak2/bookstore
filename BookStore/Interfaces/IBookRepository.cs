using BookStore.Model;

namespace BookStore.Interfaces
{
    interface IBookRepository
    {
        void Connect();
        int Insert(Book book);
        
    }
}
