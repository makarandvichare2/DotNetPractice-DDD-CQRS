using Ardalis.Result;
using Ardalis.SharedKernel;

namespace BikeShop.UseCases.Bikes.List;

public record ListBikeQuery() : IQuery<Result<IEnumerable<BikeListDTO>>>;
