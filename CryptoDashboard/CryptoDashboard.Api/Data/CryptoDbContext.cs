using Microsoft.EntityFrameworkCore;
using CryptoDashboard.Api.Models;

namespace CryptoDashboard.Api.Data
{
    public class CryptoDbContext : DbContext
    {
        public CryptoDbContext(DbContextOptions<CryptoDbContext> options) : base(options) { }

        public DbSet<CryptoPair> CryptoPairs { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }

      
    }
}
