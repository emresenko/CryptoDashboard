using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoDashboard.Api.Models;

namespace CryptoDashboard.Api.Services
{
    public interface ICryptoService
    {
        Task<(string pair, decimal percentage)> GetHighestAverageDifferenceAsync(DateTime startDate, DateTime endDate);
        Task<(decimal lowestPrice, decimal highestPrice)> GetPriceRangeAsync(string pair, DateTime startDate, DateTime endDate);
        Task<IEnumerable<CryptoPair>> GetPriceLineChartsAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<CryptoPair>> GetPairDataAsync(string pair, DateTime startDate, DateTime endDate);
    }
}
