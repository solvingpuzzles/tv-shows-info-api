using System;
using Microsoft.AspNetCore.Mvc;

namespace TvShowsApi.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new {healthcheck = DateTime.Now.ToString("u")});
        }
    }
}
