using Ardalis.Result;
using Ardalis.SharedKernel;

namespace BikeShop.UseCases.Bikes.Get;

public class GetBikeHandler(IGetBikeQueryService _query)
  : IQueryHandler<GetBikeQuery, Result<CreateBikeDataDTO>>
{
  public async Task<Result<CreateBikeDataDTO>> Handle(GetBikeQuery request, CancellationToken cancellationToken)
  {
    var result = await _query.BikeDataAsync();

    return Result.Success(result);
  }
}
