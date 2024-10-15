using Store.Data.Entities;
using Store.Reposatory.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatory.Interfaces
{
    public interface IGenericRepository< TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task <TEntity> GetByIdAsync(TKey? id );

        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetWithSpecificByIdAsync(ISpecification<TEntity> specs);
        Task<int> CountSpcificationAsync(ISpecification<TEntity> specs);
        Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs);

        Task AddAsync( TEntity entity );
        void Delete( TEntity entity );

        void  Update( TEntity entity );
    }
}
