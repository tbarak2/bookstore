using BookStore.Interfaces;
using BookStore.Model;

namespace BookStore.Connections
{
    class DaoService : IDaoService
    {
        private readonly IRepoistoryFactory _repoistoryFactory;
        private IBookRepository _bookRepository;

        public DaoService(IRepoistoryFactory repoistoryFactory)
        {
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
