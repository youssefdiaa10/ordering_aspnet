using Ordering.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ordering.Repository.Data
{
    public class StoreDbContextSeed
    {

        public static async Task SeedAsync(StoreDbContext _context)
        {

            if (_context.Products.Count() == 0)
            {
                // 1. Read Data from Json File
                var productData = File.ReadAllText("../Ordering.Repository/Data/DataSeeding/products.json");


                // 2. Convert Json string to the Needed Type
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (products?.Count() > 0)
                {
                    foreach (var item in products)
                    {
                        _context.Set<Product>().Add(item);
                    }

                    await _context.SaveChangesAsync();
                }
            }




            //==========================================


            if (_context.ProductBrands.Count() == 0)
            {
                // 1. Read Data from Json File
                var brands = File.ReadAllText("../Ordering.Repository/Data/DataSeeding/brands.json");


                // 2. Convert Json string to the Needed Type
                var products2 = JsonSerializer.Deserialize<List<Product>>(brands);
                if (products2?.Count() > 0)
                {
                    foreach (var item in products2)
                    {
                        _context.Set<Product>().Add(item);
                    }

                    await _context.SaveChangesAsync();
                }

            }



            //==========================================

            if (_context.ProductCategories.Count() == 0)
            {
                // 1. Read Data from Json File
                var categories = File.ReadAllText("../Ordering.Repository/Data/DataSeedingqg'/categories.json");


                // 2. Convert Json string to the Needed Type
                var products3 = JsonSerializer.Deserialize<List<Product>>(categories);
                if (products3?.Count() > 0)
                {
                    foreach (var item in products3)
                    {
                        _context.Set<Product>().Add(item);
                    }

                    await _context.SaveChangesAsync();
                }

            }



        }

    }
}
