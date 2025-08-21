using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarBook.Domain.Entities;

namespace CarBook.Application.Interfaces.CarDescriptionInterfaces
{
	public interface ICarDescriptionRepository
	{
        // önceki: Task<CarDescription> GetCarDescription(int carId);
        Task<CarDescription?> GetCarDescriptionAsync(int carId, CancellationToken ct = default);

    }
}
