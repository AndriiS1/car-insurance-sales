using CarInsuranceSales.Infrastructure;
using CarInsuranceSales.UseCases;
using CarInsuranceSales.UseCases.CommandRouter;
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

app.MapPost("/bot/update", async ([FromBody] Update update, [FromServices] ICommandRouter commandRouter) =>
{
    await commandRouter.Process(update);
    
    return Results.Ok();
});

await app.SetWebhookUrl();

app.Run();