using DataSynchronization.Application.UseCases.Get;
using DataSynchronization.Application.UseCases.Search;
using DataSynchronization.Application.UseCases.Synchronize;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataSynchronizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DataSynchronizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}", Name = nameof(GetAsync))]
        public async Task<IActionResult> GetAsync([FromRoute] string id)
        {
            var result = await _mediator.Send(new GetSingleQuery() { RowKey = id });
            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpGet(Name = nameof(SearchAsync))]
        public async Task<IEnumerable<SearchQueryResponse>> SearchAsync([FromQuery] DateTime fromUtc, [FromQuery] DateTime toUtc) => 
            await _mediator.Send(new SearchQuery() { FromUtc = fromUtc, ToUtc = toUtc });

        [HttpPost(Name = nameof(SyncAsync))]
        public async Task<Unit> SyncAsync() => await _mediator.Send(new SynchronizeCommand());
    }
}