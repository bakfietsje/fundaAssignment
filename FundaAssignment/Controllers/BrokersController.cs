using FundaAssignment.DTOs;
using FundaAssignment.Filters;
using FundaAssignment.Queries;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace FundaAssignment.Controllers;

[Route("api/[controller]")]
public class BrokersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<BrokerDto>), StatusCodes.Status200OK)]
    // CI/CD
    public async Task<IActionResult> Get(
        [FromQuery] BrokersFilters filters,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var brokersQuery = new BrokersQuery(
            filters.SearchQuery,
            filters.Type);
        
        var result = await mediator.Send(brokersQuery, cancellationToken);
        
        return Ok(result);
    }
}