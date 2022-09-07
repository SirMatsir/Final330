using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final330.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("{code}")]
        [HttpGet]
        public IActionResult Error(int code)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var ex = feature.Error;

            return new ObjectResult(new ApiResponse(code, ex.Message));
        }

        //public IActionResult Error2(int code)
        //{
        //    return new ObjectResult(new ApiResponse(code));
        //} 
    }
}
