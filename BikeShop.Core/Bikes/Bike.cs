using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace BikeShop.Core.Bikes
{
    public class Bike : EntityBase,IAggregateRoot
    {
        public Bike() { }
        public Bike(int manufacturerId,
        int bikeModelId,
        int categoryId,
        Price price,
        Weight weight,
        Colour colour,
        string imgUrl,
        Guid @ref
        )
        { 
            ManufacturerId = Guard.Against.NegativeOrZero(manufacturerId, nameof(manufacturerId)); 
            BikeModelId = Guard.Against.NegativeOrZero(bikeModelId, nameof(bikeModelId));
            CategoryId = Guard.Against.NegativeOrZero(categoryId, nameof(categoryId));

            Guard.Against.NegativeOrZero(price.PriceValue, nameof(Price.PriceValue));
            Guard.Against.NullOrWhiteSpace(price.Currency, nameof(Price.Currency));

            Guard.Against.NegativeOrZero(weight.WeightValue, nameof(Weight.WeightValue));
            Guard.Against.NullOrWhiteSpace(weight.Unit, nameof(Weight.Unit));

            Guard.Against.NullOrWhiteSpace(colour.ColourCode, nameof(Colour.ColourCode));

            Guard.Against.NullOrWhiteSpace(imgUrl, nameof(imgUrl));

            Price = price;
            Weight = weight;
            Colour = colour;
            ImgUrl = imgUrl;
            Ref = @ref;
        }
        public int ManufacturerId { get; private set; }
        public int BikeModelId { get; private set; }
        public int CategoryId { get; private set; }
        public string ImgUrl { get; private set; }
        public Guid Ref { get; private set; }
        public Weight Weight { get; private set; }
        public Price Price { get; private set; } 
        public Colour Colour { get; private set; }
        public void UpdateColour(string colour) => Colour = new Colour(colour);
        public void UpdateWeight(string unit, decimal weightValue) => Weight = new Weight(unit,weightValue);
        public void UpdatePrice(string currency, decimal priceValue) => Price = new Price(currency, priceValue);
    }
}
