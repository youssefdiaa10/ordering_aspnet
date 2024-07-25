using Ordering.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Core.Specifications.Product_Specs
{
    public class ProductWithFilterationForCountSpec : BaseSpecification<Product>
    {

        public ProductWithFilterationForCountSpec(ProductSpecParams productSpec)
               : base(
                p =>
                (!productSpec.BrandId.HasValue || p.BrandId == productSpec.BrandId.Value)
                &&
                (!productSpec.CategoryId.HasValue || p.CategoryId == productSpec.CategoryId.Value)
                )
        { 
        }

    }
}
