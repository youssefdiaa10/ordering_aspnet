using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.APIs.Errors;

namespace Ordering.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : BaseController
    {
        // errors/404
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code, "Endpoint"));
        }

    }
}
