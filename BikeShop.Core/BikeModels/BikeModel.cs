using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace BikeShop.Core.BikeModels
{
    public class BikeModel(string modelName) : EntityBase, IAggregateRoot
    {
        public string ModelName { get; private set; } = Guard.Against.NullOrEmpty(modelName, nameof(modelName));
    }

}
