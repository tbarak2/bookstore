using BookStore.Model;

namespace BookStore.Interfaces
{
    interface IDaoService
    {
        public void Connect();

        public int Insert(Book book);
    }
}
