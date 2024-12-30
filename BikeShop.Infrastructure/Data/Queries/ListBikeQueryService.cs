using BikeShop.UseCases.Bikes.List;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Infrastructure.Data.Queries;

public class ListBikeQueryService(AppDbContext _db) : IListBikeQueryService
{
  public async Task<IEnumerable<BikeListDTO>> ListAsync()
  {
    var result = await _db.Database.SqlQuery<BikeListDTO>(
      @$"SELECT 
                Manufacturers.Name manufacturer,
                ref,
                BikeModels.ModelName model,
                Categories.Name category,
                (Price_Currency + Format(Price_PriceValue,'##,###.00')) price,
                ColourCode colour,
                (Format(Weight_WeightValue,'##,###.00') + Weight_Unit) weight,
                ImgUrl img_url,
                Price_PriceValue priceNumeric
        FROM Bikes 
            Join BikeModels on Bikes.BikeModelId = BikeModels.Id
            Join Categories on Bikes.CategoryId = Categories.Id
            Join Manufacturers on Bikes.ManufacturerId = Manufacturers.Id
        Order by Manufacturers.Name
        ") 
      .ToListAsync();

    return result;
  }
}
