namespace BikeShop.UseCases.Bikes.Get;

public record CreateBikeDTO(
    int manufacturerId,
    int modelId,
    int categoryId,
    decimal price,
    string currency,
    string colour,
    decimal weight,
    string unit,
    string img_url);
