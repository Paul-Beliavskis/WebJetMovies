using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebJetMovies.Application.Requests;

namespace WebJetMovies.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly IMediator _mediator;

        public MovieController(ILogger logger, IMediator mediator)
        {
            _logger = logger;

            _mediator = mediator;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Get(CancellationToken cancelationToken = default)
        {
            //When the error occures it is picked up by the exception handling middleware and a bad request will be returned
            var response = await _mediator.Send(new GetCheapestMoviesQuery());

            return Ok(response);
        }
    }
}
