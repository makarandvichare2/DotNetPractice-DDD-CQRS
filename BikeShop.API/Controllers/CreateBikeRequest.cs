namespace BikeShop.API.Controllers
{
    public record CreateBikeRequest(
    int manufacturerId,
    int bikeModelId,
    int categoryId,
    decimal price,
    string currency,
    string colour,
    decimal weight,
    string unit,
    string img_url);
}
