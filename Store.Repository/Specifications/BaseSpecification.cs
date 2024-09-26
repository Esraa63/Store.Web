using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification( Expression<Func<T, bool>> criteria) 
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includs { get; } = new List<Expression<Func<T, object>>>();
    
        protected void AddInclude(Expression<Func<T, object>> includeExpression) 
            => Includs.Add(includeExpression);
    }
}
