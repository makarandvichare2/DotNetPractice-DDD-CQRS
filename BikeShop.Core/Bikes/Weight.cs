using Ardalis.SharedKernel;

namespace BikeShop.Core.Bikes
{
    public class Weight(string unit, decimal weightValue) : ValueObject
    {
        public string Unit { get; private set; } = unit;
        public decimal WeightValue { get; private set; } = weightValue;
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Unit;
            yield return WeightValue;
        }
    }
}
