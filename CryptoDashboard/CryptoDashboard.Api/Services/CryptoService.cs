using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CryptoDashboard.Api.Data;
using CryptoDashboard.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CryptoDashboard.Api.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly CryptoDbContext _context;
        private readonly IDistributedCache _cache;

        public CryptoService(CryptoDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<(string pair, decimal percentage)> GetHighestAverageDifferenceAsync(DateTime startDate, DateTime endDate)
        {
            string cacheKey = $"highest_avg_diff_{startDate.ToShortDateString()}_{endDate.ToShortDateString()}";
            var cachedResult = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResult))
            {
                return JsonSerializer.Deserialize<(string pair, decimal percentage)>(cachedResult);
            }

            var result = await _context.CryptoPairs
                .Where(cp => cp.Date >= startDate && cp.Date <= endDate)
                .GroupBy(cp => cp.PairName)
                .Select(g => new
                {
                    Pair = g.Key,
                    AverageDifference = g.Average(cp => Math.Abs(cp.Open - cp.Close) / cp.Open * 100)
                })
                .OrderByDescending(g => g.AverageDifference)
                .FirstOrDefaultAsync();

            var resultTuple = (result.Pair, result.AverageDifference);
            var serializedResult = JsonSerializer.Serialize(resultTuple);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
            await _cache.SetStringAsync(cacheKey, serializedResult, cacheOptions);

            return resultTuple;
        }

        public async Task<(decimal lowestPrice, decimal highestPrice)> GetPriceRangeAsync(string pair, DateTime startDate, DateTime endDate)
        {
            string cacheKey = $"price_range_{pair}_{startDate.ToShortDateString()}_{endDate.ToShortDateString()}";
            var cachedResult = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResult))
            {
                return JsonSerializer.Deserialize<(decimal lowestPrice, decimal highestPrice)>(cachedResult);
            }

            var result = await _context.CryptoPairs
                .Where(cp => cp.PairName == pair && cp.Date >= startDate && cp.Date <= endDate)
                .Select(cp => new
                {
                    cp.Price
                })
                .ToListAsync();

            var lowestPrice = result.Min(r => r.Price);
            var highestPrice = result.Max(r => r.Price);

            var resultTuple = (lowestPrice, highestPrice);
            var serializedResult = JsonSerializer.Serialize(resultTuple);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
            await _cache.SetStringAsync(cacheKey, serializedResult, cacheOptions);

            return resultTuple;
        }

        public async Task<IEnumerable<CryptoPair>> GetPriceLineChartsAsync(DateTime startDate, DateTime endDate)
        {
            string cacheKey = $"price_line_charts_{startDate.ToShortDateString()}_{endDate.ToShortDateString()}";
            var cachedResult = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResult))
            {
                return JsonSerializer.Deserialize<IEnumerable<CryptoPair>>(cachedResult);
            }

            var result = await _context.CryptoPairs
                .Where(cp => cp.Date >= startDate && cp.Date <= endDate)
                .OrderBy(cp => cp.Date)
                .ToListAsync();

            var serializedResult = JsonSerializer.Serialize(result);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
            await _cache.SetStringAsync(cacheKey, serializedResult, cacheOptions);

            return result;
        }

        public async Task<IEnumerable<CryptoPair>> GetPairDataAsync(string pair, DateTime startDate, DateTime endDate)
        {
            string cacheKey = $"pair_data_{pair}_{startDate.ToShortDateString()}_{endDate.ToShortDateString()}";
            var cachedResult = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResult))
            {
                return JsonSerializer.Deserialize<IEnumerable<CryptoPair>>(cachedResult);
            }

            var result = await _context.CryptoPairs
                .Where(cp => cp.PairName == pair && cp.Date >= startDate && cp.Date <= endDate)
                .OrderByDescending(cp => cp.Date)
                .ToListAsync();

            var serializedResult = JsonSerializer.Serialize(result);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
            await _cache.SetStringAsync(cacheKey, serializedResult, cacheOptions);

            return result;
        }
    }
}
