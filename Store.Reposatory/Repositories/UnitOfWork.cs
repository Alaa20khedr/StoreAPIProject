using Store.Data.Context;
using Store.Data.Entities;
using Store.Reposatory.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatory.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext context;
        private Hashtable repositories;
        public UnitOfWork(StoreContext context)
        {
            this.context = context;
        }
        public async Task<int> CompleteAsync()
        =>await context.SaveChangesAsync();

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
           if(repositories == null) 
                repositories= new Hashtable();
           var entitykey=typeof(TEntity).Name;
            if (!repositories.ContainsKey(entitykey))
            {
                var repositoryType=typeof(GenericRepository<,>);
                var repositoryInstance=Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)), context);
                repositories.Add(entitykey, repositoryInstance);
            }
            return (IGenericRepository<TEntity, TKey>)repositories[entitykey];

        }
    }
}
