using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace BikeShop.Core.Manufacturers
{
    public class Manufacturer(string name) : EntityBase, IAggregateRoot
    {
        public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    }
}
