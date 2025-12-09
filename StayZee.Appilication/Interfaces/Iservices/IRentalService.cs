using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.DTOs.ResponseDTO;

namespace StayZee.Application.Interfaces.Iservices
{
    public interface IRentalService
    {
        Task<RentalResponse> CreateRental(CreateRentalRequest request);
        Task<List<RentalCardDTO>> GetAllRentals();

    }
}
