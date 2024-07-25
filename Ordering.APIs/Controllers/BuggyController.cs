using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.APIs.Errors;
using Ordering.Repository.Data;

namespace Ordering.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }


        [HttpGet("notfound")] // GET : /api/Buggy/notfound
        public ActionResult GetNotFoundRequest()
        {
            var product = _context.Products.Find(1000);

            if (product is null)
            {
                //return NotFound(new {Message = "Not Found", Code = 404});
                return NotFound(new ApiResponse(404));
            }

            return Ok(product);
        }


        [HttpGet("servererror")] // GET : /api/Buggy/servererror
        public ActionResult GetServerError()
        {
            var product = _context.Products.Find(1000);

            var result = product.ToString(); // Will throw Exception [NullReferenceException]

            return Ok(result);
        }


        [HttpGet("badrequest")] // GET : /api/Buggy/badrequest
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }


        [HttpGet("badrequest/{id}")] // GET : /api/Buggy/badrequest/2
        public ActionResult GetBadRequest(int? id) // Validation Error
        {
            return Ok();
        }



        [HttpGet("unauthorized")] // GET : /api/Buggy/unauthorized
        public ActionResult GetUnauthorizedError(int? id) // Validation Error
        {
            return Unauthorized(new ApiResponse(401));
        }


    }
}
