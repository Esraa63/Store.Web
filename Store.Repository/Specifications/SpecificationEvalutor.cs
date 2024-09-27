using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications
{
    public class SpecificationEvalutor<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity> specs)
        {
            var query = inputQuery;
            if(specs.Criteria is not null)
                query=query.Where(specs.Criteria);
            query = specs.Includs.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));
            return query;
        }
    }
}
