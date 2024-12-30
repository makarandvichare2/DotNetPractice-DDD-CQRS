using Ardalis.SharedKernel;
using BikeShop.Core.Bikes;
using BikeShop.Core.BikeModels;
using BikeShop.Core.Categories;
using BikeShop.Core.Manufacturers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BikeShop.Infrastructure.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options,
  IDomainEventDispatcher? dispatcher) : DbContext(options)
{
    private readonly IDomainEventDispatcher? _dispatcher = dispatcher;
    public DbSet<Bike> Bikes => Set<Bike>();
    public DbSet<BikeModel> BikeModels => Set<BikeModel>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    if (_dispatcher == null) return result;

    var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
        .Select(e => e.Entity)
        .Where(e => e.DomainEvents.Any())
        .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges() =>
        SaveChangesAsync().GetAwaiter().GetResult();
}
