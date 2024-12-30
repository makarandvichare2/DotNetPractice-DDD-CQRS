using BikeShop.UseCases.Bikes.Create;
using FluentValidation;

namespace BikeShop.API.Controllers;
public class CreateBikeValidator : AbstractValidator<CreateBikeCommand>
{
    public CreateBikeValidator()
    {
        RuleFor(x => x.bikeModelId)
          .GreaterThan(0)
          .WithMessage("Bike Model is required.");
        RuleFor(x => x.categoryId)
          .GreaterThan(0)
          .WithMessage("Category is required.");
        RuleFor(x => x.manufacturerId)
         .GreaterThan(0)
         .WithMessage("Manufacturer is required.");
        RuleFor(x => x.priceValue)
         .GreaterThan(0)
         .WithMessage("Price is required.");
        RuleFor(x => x.priceCurrency)
         .NotEmpty()
         .WithMessage("Currency is required.")
         .MaximumLength(1)
         .MinimumLength(1);
        RuleFor(x => x.weightValue)
         .NotEmpty()
         .WithMessage("Weight is required.");
        RuleFor(x => x.weightUnit)
         .NotEmpty()
         .WithMessage("Weight unit is required.")
         .MaximumLength(2)
         .MinimumLength(2);
        RuleFor(x => x.colour)
         .NotEmpty()
         .WithMessage("Colour is required.");
        RuleFor(x => x.imgUrl)
         .NotEmpty()
         .WithMessage("Image url is required.");
    }
}
