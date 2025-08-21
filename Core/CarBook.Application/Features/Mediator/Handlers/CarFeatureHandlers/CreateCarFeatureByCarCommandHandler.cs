using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CarBook.Application.Features.Mediator.Commands.CarFeatureCommands;
using CarBook.Application.Interfaces.CarFeatureInterfaces;
using CarBook.Domain.Entities;

namespace CarBook.Application.Features.Mediator.Handlers.CarFeatureHandlers
{
    public class CreateCarFeatureByCarCommandHandler
        : IRequestHandler<CreateCarFeatureByCarCommand, Unit>
    {
        private readonly ICarFeatureRepository _repository;

        public CreateCarFeatureByCarCommandHandler(ICarFeatureRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateCarFeatureByCarCommand request, CancellationToken cancellationToken)
        {
            var entity = new CarFeature
            {
                Available = false,
                CarID = request.CarID,
                FeatureID = request.FeatureID
            };

            await _repository.CreateCarFeatureByCarAsync(entity, cancellationToken);
            return Unit.Value;
        }
    }
}
