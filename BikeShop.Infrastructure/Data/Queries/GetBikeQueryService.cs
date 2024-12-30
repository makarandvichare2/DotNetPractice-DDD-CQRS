using BikeShop.UseCases.Bikes;
using BikeShop.UseCases.Bikes.Get;
using BikeShop.UseCases.Bikes.List;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Infrastructure.Data.Queries;

public class GetBikeQueryService(AppDbContext _db) : IGetBikeQueryService
{
  public async Task<CreateBikeDataDTO> BikeDataAsync()
  {
    var manufacturers = await _db.Database.SqlQuery<LookUpDTO<int>>(
      $"SELECT Id, Name FROM Manufacturers Order by Name").ToListAsync();

    var bikeModels = await _db.Database.SqlQuery<LookUpDTO<int>>(
        $"SELECT Id, ModelName as Name FROM BikeModels Order by ModelName").ToListAsync();

    var categories = await _db.Database.SqlQuery<LookUpDTO<int>>(
        $"SELECT Id, Name FROM Categories Order by Name").ToListAsync();
    var colours = new List<string>
    {
        "Red",
        "Blue",
        "Green"
    };
    var currencies = new List<string>
    {
        "£",
        "$",
        "€"
    };

    var units = new List<string>
    {
        "kg",
        "tn",
        "gm"
    };

    var bike = new CreateBikeDTO(manufacturers.FirstOrDefault().Id,
                                 bikeModels.FirstOrDefault().Id,
                                 categories.FirstOrDefault().Id,
                                 0,
                                 currencies.FirstOrDefault(),
                                 colours.FirstOrDefault(),
                                 0,
                                 units.FirstOrDefault(),
                                 string.Empty);

        var createBikeDto = new CreateBikeDataDTO(bike,
                                                  bikeModels,
                                                  categories,
                                                  manufacturers,
                                                  colours,
                                                  currencies,
                                                  units);
    return createBikeDto;
  }
}
