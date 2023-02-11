using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Repositroy
{
    public interface IAsyncRepositoryBase<T>
    {
        public Task<User> GetByIdAsync(Guid id);
        public Task<User> UpdateAsync(T entity);

    }
}
