using CryptoDashboard.Api.Data;
using CryptoDashboard.Api.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
//konteyner kýsmý

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// postgre tanýmala
builder.Services.AddDbContext<CryptoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// redis tanýmlama
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "CryptoDashboard_";
});

// mongodb tanýmlama
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
