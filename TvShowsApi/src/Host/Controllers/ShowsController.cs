using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TvShows.Host.Models;
using TvShowsApi.Data.Models;
using TvShowsApi.Services;

namespace TvShowsApi.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowsService _service;
        private readonly ILogger _logger;

        public ShowsController(IShowsService service, ILogger<ShowsController> logger)
        {
            _service = service;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? page)
        {
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostTvShowsDto shows)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.AddTvShowsAsync(ConvertToModel(shows));

                return StatusCode((int) HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Returning InternalServerError due to an unexpected error.");
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        private List<TvShow> ConvertToModel(PostTvShowsDto shows)
        {
            return shows
                .Shows
                .Select(s => new TvShow
                {
                    Id = s.Id,
                    Name = s.Name,
                    Cast = s.Cast
                        .Select(c => new Actor
                        {
                            Id = c.Id,
                            Name = c.Name,
                            DateOfBirth = c.DateOfBirth
                        })
                        .ToList()
                })
                .ToList();
        }
    }
}
