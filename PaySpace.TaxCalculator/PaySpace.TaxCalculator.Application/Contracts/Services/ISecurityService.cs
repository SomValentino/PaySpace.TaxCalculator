using PaySpace.TaxCalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application.Contracts.Services
{
    public interface ISecurityService
    {
        Task RegisterClient(ClientRegistration clientRegistration);
        Task<(string tokenId, string token)> GenerateJwtFor(string clientId, string role);
    }
}
