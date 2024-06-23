using CryptoDashboard.Api.Data;
using CryptoDashboard.Api.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
//konteyner k�sm�

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// postgre tan�mala
builder.Services.AddDbContext<CryptoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// redis tan�mlama
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "CryptoDashboard_";
});

// mongodb tan�mlama
builder.Services.AddScoped<ILoggingService, LoggingService>();


builder.Services.AddScoped<ICryptoService, CryptoService>();

var app = builder.Build();

// swagger 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
