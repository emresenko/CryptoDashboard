using System;
using System.Threading.Tasks;

namespace CryptoDashboard.Api.Services
{
    public interface ILoggingService
    {
        Task LogRequestAsync(DateTime beginDate, DateTime endDate);
    }
}
