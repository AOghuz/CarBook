using CarBook.Application.Features.Mediator.Commands.ReviewCommands;
using CarBook.Application.Features.Mediator.Queries.ReviewQueries;
using CarBook.Application.Validators.ReviewValidators;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReviewsController(IMediator mediator) => _mediator = mediator;

        // 1) Bir arabanın tüm review'ları
        // GET /api/reviews/car/4
        [HttpGet("car/{id:int}")]
        public async Task<IActionResult> GetByCarId(int id)
        {
            var list = await _mediator.Send(new GetReviewByCarIdQuery(id));
            return Ok(list); // boşsa []
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewCommand command)
        {
            // Validator örneğini oluştur
            var validator = new CreateReviewValidator();
            var validationResult = validator.Validate(command);

            // Hataları kontrol et
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Hata yoksa devam et
            await _mediator.Send(command);

            // Hem 201 status hem mesaj dön
            return StatusCode(201, "Ekleme işlemi gerçekleşti");
        }


        // 4) Review güncelle
        // PUT /api/reviews/12
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReviewCommand command)
        {
            command.ReviewId = id;
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
