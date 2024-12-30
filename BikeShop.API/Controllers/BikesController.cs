using Ardalis.Result;
using BikeShop.UseCases.Bikes.Create;
using BikeShop.UseCases.Bikes.Get;
using BikeShop.UseCases.Bikes.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BikeShop.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BikesController : ControllerBase
{
    private readonly IMediator mediator;
    public BikesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<BikeListDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<BikeListDTO>>> GetListAsync()
    {
        Result<IEnumerable<BikeListDTO>> result = await mediator.Send(new ListBikeQuery());

        if (result.IsSuccess)
        {
            return Ok(result.Value.ToList());
        }

        return NotFound();
    }

    [HttpGet]
    [Route("GetBikeData")]
    [ProducesResponseType(typeof(CreateBikeDataDTO), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CreateBikeDataDTO>> GetBikeDataAsync()
    {
        Result<CreateBikeDataDTO> result = await mediator.Send(new GetBikeQuery());

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateBike(
            [FromBody] CreateBikeRequest request)
    {
        var createCommand = new CreateBikeCommand(
            request.manufacturerId,
            request.bikeModelId,
            request.categoryId,
            request.currency,
            request.price,
            request.unit,
            request.weight,
            request.colour,
            request.img_url,
            Guid.NewGuid()
            );
        var result = await mediator.Send(createCommand);

        return Created(string.Empty, result.Value);
    }
}