using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using BikeShop.Infrastructure.Data;
using BikeShop.Infrastructure.Data.Queries;
using BikeShop.UseCases.Bikes.Get;
using BikeShop.UseCases.Bikes.List;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BikeShop.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config)
  {
    string? connectionString = config.GetConnectionString("SqlServerConnection");
    Guard.Against.Null(connectionString);
    services.AddDbContext<AppDbContext>(options =>
     options.UseSqlServer(connectionString));

    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
    services.AddScoped<IListBikeQueryService, ListBikeQueryService>();
    services.AddScoped<IGetBikeQueryService, GetBikeQueryService>();

    return services;
  }
}
