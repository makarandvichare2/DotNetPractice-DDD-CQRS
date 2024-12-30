using Ardalis.Result;
using Ardalis.SharedKernel;

namespace BikeShop.UseCases.Bikes.Get;

public record GetBikeQuery() : IQuery<Result<CreateBikeDataDTO>>;
