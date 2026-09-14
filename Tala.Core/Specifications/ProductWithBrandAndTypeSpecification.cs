using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talab.Core.Helpers;
using Talabat.Core.Entities;

namespace Tala.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductSpecParams specParams)
            :base(P=>
                (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search)) &&
                (!specParams.BrandId.HasValue || P.ProductBrandId == specParams.BrandId) &&
                (!specParams.TypeId.HasValue || P.ProductTypeId == specParams.TypeId)
            )
        {
            Include.Add(P => P.ProductBrand);
            Include.Add(P => P.ProductType);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch(specParams.Sort)
                {
                case "priceAsc":                    
                    AddOrderBy(P => P.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(P => P.Price);
                    break;
                default:
                    AddOrderBy(P => P.Name);
                    break;
                }
            }
            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
        public ProductWithBrandAndTypeSpecification(int id):base(P => P.Id == id)
        {
            Include.Add(P => P.ProductBrand);
            Include.Add(P => P.ProductType);
        }
    }
}
