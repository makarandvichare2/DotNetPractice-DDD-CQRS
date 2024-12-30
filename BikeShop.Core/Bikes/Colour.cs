using Ardalis.SharedKernel;

namespace BikeShop.Core.Bikes
{
    public class Colour(string colourCode) : ValueObject
    {
        public string ColourCode { get; private set; } = colourCode;
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ColourCode;
        }
    }
}
