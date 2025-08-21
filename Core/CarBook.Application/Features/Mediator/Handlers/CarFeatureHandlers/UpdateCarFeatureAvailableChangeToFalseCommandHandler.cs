using CarBook.Application.Features.Mediator.Commands.CarFeatureCommands;
using CarBook.Application.Interfaces.CarFeatureInterfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.CarFeatureHandlers
{
    public class UpdateCarFeatureAvailableChangeToFalseCommandHandler
        : IRequestHandler<UpdateCarFeatureAvailableChangeToFalseCommand, Unit>
    {
        private readonly ICarFeatureRepository _repository;
        public UpdateCarFeatureAvailableChangeToFalseCommandHandler(ICarFeatureRepository repository)
            => _repository = repository;

        public async Task<Unit> Handle(UpdateCarFeatureAvailableChangeToFalseCommand request, CancellationToken cancellationToken)
        {
            await _repository.ChangeCarFeatureAvailableToFalseAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
