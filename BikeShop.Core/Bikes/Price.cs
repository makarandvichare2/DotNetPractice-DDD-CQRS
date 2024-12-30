using Ardalis.SharedKernel;

namespace BikeShop.Core.Bikes
{
    public class Price(string currency, decimal priceValue) : ValueObject
    {
        public string Currency { get; private set; } = currency;
        public decimal PriceValue { get; private set; } = priceValue;
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Currency;
            yield return PriceValue;
        }
    }
}
