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
        public ProductWithBrandAndTypeSpecification()
        {
            Include.Add(P => P.ProductBrand);
            Include.Add(P => P.ProductType);
        }
        public ProductWithBrandAndTypeSpecification(int id):base(P => P.Id == id)
        {
            Include.Add(P => P.ProductBrand);
            Include.Add(P => P.ProductType);
        }
    }
}
