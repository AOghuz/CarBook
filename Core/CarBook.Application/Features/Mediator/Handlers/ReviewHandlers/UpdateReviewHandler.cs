using CarBook.Application.Features.Mediator.Commands.ReviewCommands;
using CarBook.Application.Interfaces;
using CarBook.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.ReviewHandlers
{
    public class UpdateReviewHandler : IRequestHandler<UpdateReviewCommand, Unit>
    {
        private readonly IRepository<Review> _repository;
        public UpdateReviewHandler(IRepository<Review> repository) => _repository = repository;

        public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.ReviewId);
            if (entity is null)
                throw new KeyNotFoundException($"Review not found. Id={request.ReviewId}");

            // Alanları güncelle
            entity.CustomerName = request.CustomerName;
            entity.ReviewDate = request.ReviewDate; // default geliyorsa istersen entity.ReviewDate olarak bırakabilirsin
            entity.CarID = request.CarID;
            entity.Comment = request.Comment;
            entity.RaytingValue = request.RaytingValue;
            // Eğer komutta CustomerImage da varsa:
            // entity.CustomerImage = request.CustomerImage;

            await _repository.UpdateAsync(entity); // repo ct alıyorsa: await _repository.UpdateAsync(entity, cancellationToken);
            return Unit.Value;
        }
    }
}
