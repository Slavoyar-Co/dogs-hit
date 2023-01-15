using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Repositroy
{
    public interface IRepositoryBase<T>
    {
        public Task<User> GetByIdAsync(int id);
        public Task<User> UpdateAsync(T entity);

    }
}
