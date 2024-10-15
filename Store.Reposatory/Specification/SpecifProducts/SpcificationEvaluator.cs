using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatory.Specification.SpecifProducts
{
    public class SpcificationEvaluator<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> Specs)
        {
            var query = inputQuery;
            if (Specs.Ceriteria is not null)
                query = query.Where(Specs.Ceriteria);
            if (Specs.OrderBy is not null)
                query = query.OrderBy(Specs.OrderBy);
            if (Specs.OrderByDescinding is not null)
                query = query.OrderByDescending(Specs.OrderByDescinding);

            if (Specs.IsPaginated )
                query = query.Skip(Specs.Skip).Take(Specs.Take);
            query = Specs.Includes.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));

            return query;

        }
    }
}
