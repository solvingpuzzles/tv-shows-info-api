using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TvShowsApi.Data.Models;
using TvShowsApi.Host.Models;
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
            if (page.HasValue && page.Value < 1)
            {
                return BadRequest(new { param = nameof(page), message = $"The param must have a value that's equal or greater than 1."});
            }
            
            try
            {
                var shows = await _service.GetTvShowsAsync(page);
                
                return new JsonResult(ConvertToDto(shows));
            }
            catch (TimeoutException e)
            {
                _logger.LogError("Database isn't accessible.", e);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to access the good stuff.");
            }
            catch (Exception e)
            {
                _logger.LogError("Error requesting TV Shows.", e);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
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
        
        private List<GetTvShowDto> ConvertToDto(List<TvShow> shows)
        {
            return shows
                .Select(s => new GetTvShowDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Cast = s.Cast
                        .OrderByDescending(x => x.DateOfBirth)
                        .Select(a => new GetActorDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            DateOfBirth = a.DateOfBirth.HasValue
                                ? a.DateOfBirth.Value.Date.ToString("yyyy-MM-dd")
                                : "N/A"
                        }).ToList()
                })
                .ToList();
        }
    }
}
