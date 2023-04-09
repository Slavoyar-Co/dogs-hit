namespace Infrastructure.Repositroy
{
    public interface IAsyncRepositoryBase<T>
    {
        public Task<T> GetByIdAsync(Guid id);
        public Task<T> UpdateAsync(T entity);

    }
}
