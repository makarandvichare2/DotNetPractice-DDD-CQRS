using Ardalis.Result;
using Ardalis.SharedKernel;

namespace BikeShop.UseCases.Bikes.List;

public class ListBikeHandler(IListBikeQueryService _query)
  : IQueryHandler<ListBikeQuery, Result<IEnumerable<BikeListDTO>>>
{
  public async Task<Result<IEnumerable<BikeListDTO>>> Handle(ListBikeQuery request, CancellationToken cancellationToken)
  {
    var result = await _query.ListAsync();

    return Result.Success(result);
  }
}
