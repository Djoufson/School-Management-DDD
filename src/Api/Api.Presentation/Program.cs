using Api.Application;
using Api.Infrastructure;
using Api.Presentation;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplicationLayer()
    .AddInfrastructureLayer(builder.Configuration)
    .AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.SeedDataAsync();
}

app.ConfigurePipeline();

await app.RunAsync();
