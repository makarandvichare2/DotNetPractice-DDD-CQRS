namespace BikeShop.UseCases.Bikes;

public record LookUpDTO<T>(
    T Id,
    string Name);
