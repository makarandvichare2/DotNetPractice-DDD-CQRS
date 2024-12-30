namespace BikeShop.UseCases.Bikes.Get;

public interface IGetBikeQueryService
{
  Task<CreateBikeDataDTO> BikeDataAsync();
}
