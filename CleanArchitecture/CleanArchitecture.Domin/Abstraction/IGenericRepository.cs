using CleanArchitecture.Domin.Models;
using CleanArchitecture.Domin.Specifications;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Reposatory
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task Add(T entity);
        Task AddRangeAsync(List<T> entities);
        void Delete(T entity);
        void DeleteRange(List<T> entities);
        T? GetById(int id);
        T? GetEntityWithSpec(BaseSpecifications<T> specifications);
        (IQueryable<T> data, int count) GetWithSpec(BaseSpecifications<T> specifications);
        Task<bool> IsExist(Expression<Func<T, bool>> filter);
        Task<bool> Save();
        void Update(T entity);
        void UpdateRange(List<T> entities);
    }
}