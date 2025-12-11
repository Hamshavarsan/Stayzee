using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StayZee.Domain.Entities;

namespace StayZee.Application.Interfaces.Iservices
{
    public interface IPropertySerivice
    {
        Task<List<Property>> GetPropertiesAsync(string? search, string? city, int? minBedrooms, decimal? maxPrice);
        Task<Property?> GetByIdAsync(Guid id);
        Task<Property> CreateAsync(Property p);
// Duplicate method removed
    }
}
