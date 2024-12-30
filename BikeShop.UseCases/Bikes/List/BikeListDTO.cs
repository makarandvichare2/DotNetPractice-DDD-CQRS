namespace BikeShop.UseCases.Bikes.List;

public record BikeListDTO(
    string manufacturer,
    Guid @ref,
    string model,
    string category,
    string price,
    string colour,
    string weight,
    string img_url,
    decimal priceNumeric);
