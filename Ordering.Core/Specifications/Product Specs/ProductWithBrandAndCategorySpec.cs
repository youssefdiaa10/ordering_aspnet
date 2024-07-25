﻿using Ordering.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Core.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpec : BaseSpecification<Product>
    {
        public ProductWithBrandAndCategorySpec(ProductSpecParams productSpec) 
               : base(

                     p => 
                     (string.IsNullOrEmpty(productSpec.Search) || p.Name.ToLower().Contains(productSpec.Search))
                     &&
                     (!productSpec.BrandId.HasValue || p.BrandId == productSpec.BrandId.Value) 
                     && 
                     (!productSpec.CategoryId.HasValue || p.CategoryId == productSpec.CategoryId.Value)
                     
                     )
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);


            if (!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch(productSpec.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

            else
            {
                AddOrderBy(p => p.Name);
            }


            ApplyPagination(productSpec.PageSize * (productSpec.PageSize - 1), productSpec.PageSize);
        }


        public ProductWithBrandAndCategorySpec(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
