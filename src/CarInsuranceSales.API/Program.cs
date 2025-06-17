using CarInsuranceSales.Infrastructure;
using CarInsuranceSales.UseCases;
using CarInsuranceSales.UseCases.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.ConfigureInfrastructure();
builder.ConfigureUseCases();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/bot/update", async ([FromBody] Update update, [FromServices] ICommandProcessor commandProcessor) =>
{
    await commandProcessor.Process(update);
    
    return Results.Ok();
});

await app.SetWebhookUrl();

app.Run();