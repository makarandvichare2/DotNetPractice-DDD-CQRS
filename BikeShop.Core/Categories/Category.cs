using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace BikeShop.Core.Categories
{
    public class Category(string name) : EntityBase, IAggregateRoot
    {
        public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    }
}
