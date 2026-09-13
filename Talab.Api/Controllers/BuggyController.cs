using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tala.Repository;

namespace Talab.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            var product = _context.Products.Find(42);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            var product = _context.Products.Find(42);
            var productToReturn = product.ToString();//Null Reference Exception
            return Ok(productToReturn);
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {

            return BadRequest();
        }

        [HttpGet("badrequest/{id}")] // api.../five (validation error)
        public IActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
