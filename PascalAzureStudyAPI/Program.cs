using System;
using System.Configuration;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Cosmos;
using PascalAzureStudyAPI.Repositories;
using PascalAzureStudyAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Pascal's API",
        Version = "v1"
    });
});

// Dependency injection
builder.Services.AddSingleton((provider) =>
{
    var uri = builder.Configuration.GetValue<string>("CosmosDb:Uri");
    var key = new KeyVaultService(builder.Configuration).GetCosmosDbPrimaryKey();
    return new CosmosClient(uri, key);
    // return new PortfoliosRepository(cosmosClient, "YOUR_DATABASE_NAME", "YOUR_CONTAINER_NAME");
});

builder.Services.AddTransient<IKeyVaultService, KeyVaultService>();
builder.Services.AddTransient<IPortfoliosService, PortfoliosService>();
builder.Services.AddTransient<IPortfoliosRepository, PortfoliosRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
