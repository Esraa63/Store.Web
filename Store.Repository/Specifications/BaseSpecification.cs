﻿using System;
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

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDesending { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression) 
            => Includs.Add(includeExpression);

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
            => OrderBy = orderByExpression;
        protected void AddOrderByDesending(Expression<Func<T, object>> orderByDesendingExpression)
            => OrderBy = orderByDesendingExpression;
    }
}
