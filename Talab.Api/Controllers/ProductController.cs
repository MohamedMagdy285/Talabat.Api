using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tala.Core.Specifications;
using Tala.Repository;
using Talab.Api.Dtos;
using Talab.Api.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talab.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;

        public ProductController(IMapper mapper,
            IGenericRepository<Product> productRepository
            ,IGenericRepository<ProductBrand> brandRepo
            ,IGenericRepository<ProductType> typeRepo)
        {
            //_context = context;
            _mapper = mapper;
            _productRepository = productRepository;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts(ProductSpecParams specParams)
        {
            var specs = new ProductWithBrandAndTypeSpecification(specParams);
            var products = await _productRepository.GetAllWithSpecAsync(specs);
            var productsToReturn = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(productsToReturn);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands); 
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes()
        {
            var types = await _typeRepo.GetAllAsync();
            return Ok(types);
        }
    }
}
