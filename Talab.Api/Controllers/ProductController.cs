using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tala.Core;
using Tala.Core.Specifications;
using Tala.Repository;
using Talab.Api.Dtos;
using Talab.Api.Helpers;
using Talab.Core.Helpers;
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
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IGenericRepository<Product> _productRepository;
        //private readonly IGenericRepository<ProductBrand> _brandRepo;
        //private readonly IGenericRepository<ProductType> _typeRepo;

        public ProductController(IMapper mapper,
            IUnitOfWork unitOfWork)
        //IGenericRepository<Product> productRepository
        //,IGenericRepository<ProductBrand> brandRepo
        //,IGenericRepository<ProductType> typeRepo)
        {
            //_context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            //_productRepository = productRepository;
            //_brandRepo = brandRepo;
            //_typeRepo = typeRepo;
        }
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var specs = new ProductWithBrandAndTypeSpecification(specParams);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(specs);
            var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            var countSpecs = new ProductWithFilterationForCountSpecification(specParams);
            var count = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(countSpecs);
            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex,specParams.PageSize,count,data));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands); 
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(types);
        }
    }
}
