using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CryptoDashboard.Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CryptoPairViewModel>> GetCryptoPairsAsync(DateTime startDate, DateTime endDate)
        {
            var response = await _httpClient.GetFromJsonAsync<List<CryptoPairViewModel>>($"api/crypto/highest-average-difference?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
            return response;
        }

        public async Task<List<CryptoPair>> GetCryptoPairDetailsAsync(string pairName)
        {
            var response = await _httpClient.GetFromJsonAsync<List<CryptoPair>>($"api/crypto/pair-data?pair={pairName}");
            return response;
        }
    }

    public class CryptoPairViewModel
    {
        public string PairName { get; set; }
        public decimal AverageDifference { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal HighestPrice { get; set; }
    }

    public class CryptoPair
    {
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Price { get; set; }
        public long Volume { get; set; }
        public decimal ChangePercentage { get; set; }
    }
}
