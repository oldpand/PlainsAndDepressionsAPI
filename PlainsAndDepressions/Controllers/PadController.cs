using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlainsAndDepressions.Requests;
using PlainsAndDepressions.Services.Commands;

namespace PlainsAndDepressions.Controllers;

[Route("[controller]")]
[ApiController]
public class PadController : ControllerBase
{
    private readonly IMediator _mediator;

    public PadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    [Route("calculate")]
    public async Task<IActionResult> CalculatePad([FromBody]CalculatePadRequest request)
    {
        var result = await _mediator.Send(new MeadowProcessCommand(request.Meadow));

        return Ok(result);
    }
}
