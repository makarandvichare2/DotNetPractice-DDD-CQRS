namespace BikeShop.UseCases.Bikes.Get;

public record CreateBikeDataDTO(
    CreateBikeDTO bike,
    List<LookUpDTO<int>> bikeModels,
    List<LookUpDTO<int>> categories,
    List<LookUpDTO<int>> manufacturers,
    List<string> colours,
    List<string> currencies,
    List<string> units
    );
