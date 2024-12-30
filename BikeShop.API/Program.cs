using Ardalis.ListStartupServices;
using Ardalis.SharedKernel;
using BikeShop.API.Controllers;
using BikeShop.Core.Bikes;
using BikeShop.Infrastructure;
using BikeShop.Infrastructure.Data;
using BikeShop.UseCases.Bikes.Create;
using FluentValidation;
using MediatR;
using Serilog;
using Serilog.Extensions.Logging;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureMediatR();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

    app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseShowAllServicesMiddleware();
}

app.UseAuthorization();

app.MapControllers();

await SeedDatabase(app);

app.Run();

static async Task SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        //context.Database.Migrate(); // uncomment for creating the db on running the api
        context.Database.EnsureCreated();
        await SeedData.InitializeAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

void ConfigureMediatR()
{
    var mediatRAssemblies = new[]
    {
        Assembly.GetAssembly(typeof(Bike)), // Core
        Assembly.GetAssembly(typeof(CreateBikeCommand)), // UseCases
    };
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
    builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(CreateBikeValidator)));
    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
}
