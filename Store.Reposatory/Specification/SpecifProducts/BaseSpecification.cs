using Store.Reposatory.Specification.SpecifProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatory.Specification.SpecifProducts
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T,bool>> creteria) { 

            Ceriteria = creteria;
        }
        public Expression<Func<T, bool>> Ceriteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescinding {get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
           => Includes.Add(includeExpression);
        protected void AddOrderBy(Expression<Func<T, object>> orderby)
         => OrderBy = orderby;
        protected void AddOrderByDesc(Expression<Func<T, object>> orderbydesc)
      => OrderByDescinding = orderbydesc;
        protected void ApplyPagination(int skip,int take)
        {
            Skip = skip;
            Take = take;
            IsPaginated = true;
        }
    }
}
