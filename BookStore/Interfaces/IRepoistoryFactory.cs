namespace BookStore.Interfaces
{
    interface IRepoistoryFactory
    {
        public IBookRepository CreateBookRepository();
    }
}
