using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tala.Core.Specifications;
using Tala.Repository;
using Talab.Api.Dtos;
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

        public ProductController(IMapper mapper,IGenericRepository<Product> productRepository)
        {
            //_context = context;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var specs = new ProductWithBrandAndTypeSpecification();
            var products = await _productRepository.GetAllWithSpecAsync(specs);
            var productsToReturn = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(productsToReturn);
        }
    }
}
