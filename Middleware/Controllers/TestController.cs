using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Middleware.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpPost("TestAuthentication")]
        public IActionResult TestAuthentication()
        {
            var status = HttpContext.Response.StatusCode;
            if (status == (int) HttpStatusCode.Unauthorized)
            {
                return Unauthorized("Yetkiniz yok");
            }
            return Ok("Successful!");
        }
    }
}
