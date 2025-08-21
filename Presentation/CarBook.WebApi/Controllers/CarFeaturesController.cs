using MediatR;
using Microsoft.AspNetCore.Mvc;
using CarBook.Application.Features.Mediator.Commands.CarFeatureCommands;
using CarBook.Application.Features.Mediator.Queries.CarFeatureQueries;

namespace CarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarFeaturesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CarFeaturesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> CarFeatureListByCarId([FromQuery] int id, CancellationToken ct)
        {
            var values = await _mediator.Send(new GetCarFeatureByCarIdQuery(id), ct);
            return Ok(values);
        }

        [HttpGet("CarFeatureChangeAvailableToFalse")]
        public async Task<IActionResult> CarFeatureChangeAvailableToFalse([FromQuery] int id, CancellationToken ct)
        {
            await _mediator.Send(new UpdateCarFeatureAvailableChangeToFalseCommand(id), ct);
            return Ok("Güncelleme Yapıldı");
        }

        [HttpGet("CarFeatureChangeAvailableToTrue")]
        public async Task<IActionResult> CarFeatureChangeAvailableToTrue([FromQuery] int id, CancellationToken ct)
        {
            await _mediator.Send(new UpdateCarFeatureAvailableChangeToTrueCommand(id), ct);
            return Ok("Güncelleme Yapıldı");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarFeatureByCarID([FromBody] CreateCarFeatureByCarCommand command, CancellationToken ct)
        {
            await _mediator.Send(command, ct);
            return Ok("Ekleme Yapıldı");
        }
    }
}
