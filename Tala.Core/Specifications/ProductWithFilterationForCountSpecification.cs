using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talab.Core.Helpers;
using Talabat.Core.Entities;

namespace Tala.Core.Specifications
{
    public class ProductWithFilterationForCountSpecification : BaseSpecification<Product>
    {

        public ProductWithFilterationForCountSpecification(ProductSpecParams specParams)
           : base(P =>
               (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search)) &&
               (!specParams.BrandId.HasValue || P.ProductBrandId == specParams.BrandId) &&
               (!specParams.TypeId.HasValue || P.ProductTypeId == specParams.TypeId)
           )
        {
            
        }

    }
}
