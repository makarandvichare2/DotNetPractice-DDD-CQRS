using Ardalis.SharedKernel;
using BikeShop.Core.Bikes;
using BikeShop.UseCases.Bikes.Create;
using FluentAssertions;
using FluentValidation;
using NSubstitute;

namespace BikeShop.UseCases.Tests
{
    public class CreateBikeHandlerTest
    {
        private readonly IRepository<Bike> _repository = Substitute.For<IRepository<Bike>>();
        private readonly IValidator<CreateBikeCommand> _validator = Substitute.For<IValidator<CreateBikeCommand>>();
        private CreateBikeHandler _handler;

        public CreateBikeHandlerTest()
        {
            _handler = new CreateBikeHandler(_repository, _validator);
        }
        [Fact]
        public async Task Handle_WithValidInput_CreateBike()
        {
            //Arrange
            var inputData = ValidInputs();
            var _bike = CreateBike(inputData.manufacturerId,
               inputData.bikeModelId,
               inputData.categoryId,
               inputData.price,
               inputData.currency,
               inputData.weight,
               inputData.unit,
               inputData.colour,
               inputData.@ref,
               inputData.imgUrl
               );

            var command  = new CreateBikeCommand(
            inputData.manufacturerId,
            inputData.bikeModelId,
            inputData.categoryId,
            inputData.currency,
            inputData.price,
            inputData.unit,
            inputData.weight,
            inputData.colour,
            inputData.imgUrl,
            Guid.NewGuid()
            );
            _repository.AddAsync(Arg.Any<Bike>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(_bike));

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        #region private methods
        private (int manufacturerId, int bikeModelId, int categoryId, decimal price, string currency, decimal weight, string unit, string colour, Guid @ref, string imgUrl) ValidInputs()
        {
            return (manufacturerId: 1,
            bikeModelId: 1,
            categoryId: 1,
            price: 1,
            currency: "£",
            weight: 1,
            unit: "kg",
            colour: "Red",
            @ref: Guid.NewGuid(),
            imgUrl: "/asset/photo.jpg");
        }

        private Bike CreateBike(
            int manufacturerId,
            int bikeModelId,
            int categoryId,
            decimal price,
            string currency,
            decimal weight,
            string unit,
            string colour,
            Guid @ref,
            string imgUrl
            )
        {
            return new Bike(
               manufacturerId,
               bikeModelId,
               categoryId,
               new Price(currency, price),
               new Weight(unit, weight),
               new Colour(colour),
               imgUrl,
               @ref);
        }
        #endregion
    }
}