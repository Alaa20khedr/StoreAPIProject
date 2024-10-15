using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Reposatory.Interfaces;
using Store.Reposatory.Specification;
using Store.Reposatory.Specification.SpecifProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatory.Repositories
{
  
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(TEntity entity)
       => await context.Set<TEntity>().AddAsync(entity);

        public Task<int> CountSpcificationAsync(ISpecification<TEntity> specs)
       => ApplySpecificaton(specs).CountAsync();
        public void Delete(TEntity entity)
          => context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
            => await context.Set<TEntity>().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs)
      => await ApplySpecificaton(specs).ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey? id)
      => await context.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> GetWithSpecificByIdAsync(ISpecification<TEntity> specs)
        => await ApplySpecificaton(specs).FirstOrDefaultAsync();

        public void Update(TEntity entity)
           => context.Set<TEntity>().Update(entity);

        private IQueryable<TEntity> ApplySpecificaton(ISpecification<TEntity> specs)
        =>  SpcificationEvaluator<TEntity, TKey>.GetQuery(context.Set<TEntity>(), specs);
    }
}


