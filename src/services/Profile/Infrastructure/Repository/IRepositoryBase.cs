namespace Infrastructure.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        public Task<T> Get(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task Add(T entity);
        public void Delete(T entity);
        public void Update(T entity);
    }
}