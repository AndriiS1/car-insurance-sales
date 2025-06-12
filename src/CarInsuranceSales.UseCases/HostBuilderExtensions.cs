using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace CarInsuranceSales.UseCases;

public static class HostBuilderExtensions
{
    public static void ConfigureUseCases(this WebApplicationBuilder builder)
    {
        builder.AddMediatr();
    }
    
    private static void AddMediatr(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MediatrMaker>());
    }
}
