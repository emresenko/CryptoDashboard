using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CryptoDashboard.Api.Services;
using CryptoDashboard.Api.Models;

namespace CryptoDashboard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoService _cryptoService;
        private readonly ILoggingService _loggingService;
        private readonly ILogger<CryptoController> _logger;

        public CryptoController(ICryptoService cryptoService, ILoggingService loggingService, ILogger<CryptoController> logger)
        {
            _cryptoService = cryptoService;
            _loggingService = loggingService;
            _logger = logger;
        }

        [HttpGet("highest-average-difference")]
        public async Task<IActionResult> GetHighestAverageDifference([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var result = await _cryptoService.GetHighestAverageDifferenceAsync(startDate, endDate);
                await _loggingService.LogRequestAsync(startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "En yüksek ortalama hatasi");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("price-range")]
        public async Task<IActionResult> GetPriceRange([FromQuery] string pair, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var result = await _cryptoService.GetPriceRangeAsync(pair, startDate, endDate);
                await _loggingService.LogRequestAsync(startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fiyat araligi hatasi.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("price-line-charts")]
        public async Task<IActionResult> GetPriceLineCharts([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var result = await _cryptoService.GetPriceLineChartsAsync(startDate, endDate);
                await _loggingService.LogRequestAsync(startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fiyat çizgi grafigi hatasi.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("pair-data")]
        public async Task<IActionResult> GetPairData([FromQuery] string pair, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var result = await _cryptoService.GetPairDataAsync(pair, startDate, endDate);
                await _loggingService.LogRequestAsync(startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Varlik cifti hatasi.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
