using HackerApiConnector.API.Config;
using HackerApiConnector.API.Config.Profiles;
using HackerApiConnector.Application.Services;
using HackerApiConnector.Domain.Interfaces.RestServices;
using HackerApiConnector.Domain.Interfaces.Services;
using HackerApiConnector.Infrastructure.RestService.HttpClient;
using HackerApiConnector.Infrastructure.RestService.Services;
using Microsoft.AspNetCore.Hosting.Server;
using Refit;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<HackerApiFromModelToViewModel>();
    cfg.AddProfile<HackerApiFromResponseToModel>();
});

builder.Services.AddHttpClient<IHackerApiHttpClient>();

builder.Services.AddScoped<IConnectorService, ConnectorService>();
builder.Services.AddScoped<IHackerApiRequestService, HackerApiRequestService>();

var apiSettings = builder.Configuration.GetSection("Connectionstrings").Get<Connectionstrings>();
builder.Services.AddRefitClient<IHackerApiHttpClient>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(apiSettings.HackerApi);
    });

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; 
    options.InstanceName = "HackerApiCache_";
});

var app = builder.Build();
app.UseMiddleware<Middleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
