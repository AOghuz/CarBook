using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.PricingCommands
{
    public class CreatePricingCommand : IRequest<Unit>
    {
        public string Name { get; set; }
    }
}
