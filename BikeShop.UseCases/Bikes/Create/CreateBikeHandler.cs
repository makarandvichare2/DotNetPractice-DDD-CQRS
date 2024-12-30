using Ardalis.Result;
using Ardalis.SharedKernel;
using BikeShop.Core.Bikes;
using FluentValidation;

namespace BikeShop.UseCases.Bikes.Create;

public class CreateBikeHandler(IRepository<Bike> _repository,
    IValidator<CreateBikeCommand> _validator)
  : ICommandHandler<CreateBikeCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateBikeCommand request,
      CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var newBike = new Bike(
        request.manufacturerId,
        request.bikeModelId,
        request.categoryId,
        new Price(request.priceCurrency, request.priceValue),
        new Weight(request.weightUnit, request.weightValue),
        new Colour(request.colour),
        request.imgUrl,
        request.@ref
        );
        var createdItem = await _repository.AddAsync(newBike, cancellationToken);

        return createdItem.Id;
    }
}
