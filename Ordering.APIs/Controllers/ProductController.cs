using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.APIs.DTOs;
using Ordering.APIs.Errors;
using Ordering.APIs.Helpers;
using Ordering.Core.Models;
using Ordering.Core.Repositories.Interfaces;
using Ordering.Core.Specifications;
using Ordering.Core.Specifications.Product_Specs;

namespace Ordering.APIs.Controllers
{
    public class ProductController : BaseController
    {

        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _brandsRepository;
        private readonly IGenericRepository<ProductCategory> _categoriesRepository;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepository, 
                                 IMapper mapper, 
                                 IGenericRepository<ProductBrand> brandsRepository, 
                                 IGenericRepository<ProductCategory> categoriesRepository)
        {
            _productRepository = productRepository;
            _brandsRepository = brandsRepository;
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParams productSpec)
        {
            //var products = await _productRepository.GetAllAsync();

            var spec = new ProductWithBrandAndCategorySpec(productSpec);
            var products = await _productRepository.GetAllWithSpecAsync(spec);

            ////JsonResult result = new JsonResult(products);
            //OkObjectResult result = new OkObjectResult(products);

            //result.ContentTypes = new Microsoft.AspNetCore.Mvc.Formatters.MediaTypeCollection();

            //result.StatusCode = 200;

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);

            var countSpec = new ProductWithFilterationForCountSpec(productSpec);

            int count = await _productRepository.GetCountAsync(countSpec);

            return Ok(new Pagination<ProductToReturnDTO>(productSpec.PageSize, productSpec.PageIndex, count, data));
        }


        [ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var spec = new ProductWithBrandAndCategorySpec(id);

            var product = await _productRepository.GetWithSpecAsync(spec);

            if (product is null) { return NotFound(new ApiResponse(404)); }

            var result = _mapper.Map<Product, ProductToReturnDTO>(product);

            return Ok(result);
        }


        [HttpGet("brands")] // GET : /api/products/brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandsRepository.GetAllAsync();

            return Ok(brands);
        }


        [HttpGet("categories")] // GET : /api/products/categories
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
            var categories = await _categoriesRepository.GetAllAsync();

            return Ok(categories);
        }


    }
}
