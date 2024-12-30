using Ardalis.Result;
using Ardalis.SharedKernel;

namespace BikeShop.UseCases.Bikes.Create;
public record CreateBikeCommand
        (int manufacturerId,
        int bikeModelId,
        int categoryId,
        string priceCurrency,
        decimal priceValue,
        string weightUnit,
        decimal weightValue,
        string colour,
        string imgUrl,
        Guid @ref
         ) : ICommand<Result<int>>;

