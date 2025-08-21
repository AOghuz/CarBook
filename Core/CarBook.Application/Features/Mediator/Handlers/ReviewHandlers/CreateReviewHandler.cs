using CarBook.Application.Features.Mediator.Commands.ReviewCommands;
using CarBook.Application.Interfaces;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.ReviewHandlers
{
    public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, Unit>
    {
        private readonly IRepository<Review> _repository;
        public CreateReviewHandler(IRepository<Review> repository) => _repository = repository;

        public async Task<Unit> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var entity = new Review
            {
                CustomerImage = request.CustomerImage,
                CarID = request.CarID,
                Comment = request.Comment,
                CustomerName = request.CustomerName,
                RaytingValue = request.RaytingValue,
                // Dışarıdan tarih geliyorsa onu kullan; yoksa (default) bugünün tarihini ata
                ReviewDate = request.ReviewDate == default ? DateTime.UtcNow.Date : request.ReviewDate
            };

            await _repository.CreateAsync(entity); // repo ct almıyorsa böyle bırak
            // await _repository.CreateAsync(entity, cancellationToken); // ct alıyorsa bu şekilde

            return Unit.Value;
        }
    }
}
