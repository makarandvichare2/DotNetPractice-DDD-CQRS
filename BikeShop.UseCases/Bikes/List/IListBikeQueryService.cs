namespace BikeShop.UseCases.Bikes.List;

public interface IListBikeQueryService
{
  Task<IEnumerable<BikeListDTO>> ListAsync();
}
