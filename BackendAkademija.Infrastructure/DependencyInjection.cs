using BackendAkademija.Application.Interfaces;
using BackendAkademija.Infrastructure.Auth;
using BackendAkademija.Infrastructure.ExternalServices.DummyJson;
using BackendAkademija.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackendAkademija.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastractureDi(this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration["DummyJson:BaseUrl"];

        services.AddHttpClient<IProductSource, DummyJsonProductsSource>(client =>
        {
            if (baseUrl != null) client.BaseAddress = new Uri(baseUrl);
        });

        services.AddHttpClient<IAuthService, DummyJsonAuthService>(client =>
        {
            if(baseUrl != null) client.BaseAddress = new Uri(baseUrl);
        });
        
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IRefreshTokenStore, RefreshTokenStore>();
        
        return services;

    }
}