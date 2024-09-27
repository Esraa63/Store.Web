using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications.ProductSpecs
{
    public class ProductWithSpecifications : BaseSpecification<Product>
    {
        public ProductWithSpecifications(ProductSpecification specs)
            : base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId)
                       && (!specs.TypeId.HasValue || product.TypeId == specs.TypeId) )
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
            AddOrderBy(x => x.Name);
            if(!string.IsNullOrEmpty(specs.Sort))
            {
                switch(specs.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price); 
                        break;
                    case "priceDesc":
                        AddOrderByDesending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }
        public ProductWithSpecifications(int? id):base(product => product.Id ==id)
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
        }
    }
}
