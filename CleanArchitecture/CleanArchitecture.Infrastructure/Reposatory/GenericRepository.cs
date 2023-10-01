using CleanArchitecture.Domin.Features.Base.Abstraction;
using CleanArchitecture.Domin.Specifications;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Reposatory
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<T> _entity;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }
        public async Task Add(T entity) => await _entity.AddAsync(entity);
        public async Task AddRangeAsync(List<T> entities) => await _entity.AddRangeAsync(entities);
        public void Delete(T entity) => _entity.Remove(entity);
        public void DeleteRange(List<T> entities) => _entity.RemoveRange(entities);
        public void Update(T entity) => _entity.Update(entity);
        public void UpdateRange(List<T> entities) => _entity.UpdateRange(entities);
        public T? GetById(int id) => _entity.Find(id);
        public (IQueryable<T> data, int count) GetWithSpec(BaseSpecifications<T> specifications) => SpecificationEvaluator<T>.GetQuery(_entity, specifications);
        public T? GetEntityWithSpec(BaseSpecifications<T> specifications) => SpecificationEvaluator<T>.GetQuery(_entity, specifications).data.FirstOrDefault();
        public async Task<bool> IsExist(Expression<Func<T, bool>> filter) => await _entity.AnyAsync(filter);
        public async Task<bool> Save() => await _context.SaveChangesAsync() > 0;
    }
}


