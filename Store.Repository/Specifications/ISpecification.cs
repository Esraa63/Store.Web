using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications
{
    public interface ISpecification<T>
    {
        //Criteria .Where(x => x.Id == id)
         Expression<Func<T, bool>> Criteria { get; }

        //Includs
        List <Expression<Func<T,object>>> Includs { get; }
      
        Expression<Func<T,object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDesending { get; }
        int Take { get; }   
        int Skip { get; }
        bool IsPaginated { get; }

    }
}
