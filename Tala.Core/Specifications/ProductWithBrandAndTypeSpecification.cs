using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Tala.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(string? sort, int? brandId, int? typeId)
            :base(P=>
                (!brandId.HasValue || P.ProductBrandId == brandId) &&
                (!typeId.HasValue || P.ProductTypeId == typeId)
            )
        {
            Include.Add(P => P.ProductBrand);
            Include.Add(P => P.ProductType);

            if (!string.IsNullOrEmpty(sort))
            {
                switch(sort)
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
        }
        public ProductWithBrandAndTypeSpecification(int id):base(P => P.Id == id)
        {
            Include.Add(P => P.ProductBrand);
            Include.Add(P => P.ProductType);
        }
    }
}
