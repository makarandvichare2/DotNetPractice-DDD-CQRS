using BikeShop.Core.Bikes;
using FluentAssertions;
namespace BikeShop.Core.Tests
{
    public class BikeTests
    {
        [Fact]
        public void Constructor_WithValidInputs_CreateBikeInstance()
        {
            //Arrange
            var inputData = ValidInputs();

            //Act
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

            //Assert
            inputData.manufacturerId.Should().Be(_bike.ManufacturerId);
            inputData.bikeModelId.Should().Be(_bike.BikeModelId);
            inputData.categoryId.Should().Be(_bike.CategoryId);
            inputData.price.Should().Be(_bike.Price.PriceValue);
            inputData.currency.Should().Be(_bike.Price.Currency);
            inputData.weight.Should().Be(_bike.Weight.WeightValue);
            inputData.unit.Should().Be(_bike.Weight.Unit);
            inputData.colour.Should().Be(_bike.Colour.ColourCode);
            inputData.@ref.Should().Be(_bike.Ref);
            inputData.imgUrl.Should().Be(_bike.ImgUrl);
        }

        [Fact]
        public void Constructor_WithZeroManufacturerId_ThrowException()
        {
            //Arrange
             var inputData = ValidInputs();
            inputData.manufacturerId = 0;

           //Act
           Action act = () => CreateBike(
               inputData.manufacturerId,
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

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Required input manufacturerId cannot be zero or negative. (Parameter 'manufacturerId')");
        }

        [Fact]
        public void Constructor_WithZeroBikeModelId_ThrowException()
        {
            //Arrange
            var inputData = ValidInputs();
            inputData.bikeModelId = 0;

            //Act
            Action act = () => CreateBike(
                inputData.manufacturerId,
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

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Required input bikeModelId cannot be zero or negative. (Parameter 'bikeModelId')");
        }

        [Fact]
        public void Constructor_WithZeroPriceValue_ThrowException()
        {
            //Arrange
            var inputData = ValidInputs();
            inputData.price = 0;

            //Act
            Action act = () => CreateBike(
                inputData.manufacturerId,
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

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Required input PriceValue cannot be zero or negative. (Parameter 'PriceValue')");
        }

        [Fact]
        public void Constructor_WithInvalidCurrency_ThrowException()
        {
            //Arrange
            var inputData = ValidInputs();
            inputData.currency = string.Empty;

            //Act
            Action act = () => CreateBike(
                inputData.manufacturerId,
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

            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            exception.Message.Should().Be("Required input Currency was empty. (Parameter 'Currency')");
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
               new Weight(unit,weight),
               new Colour(colour),
               imgUrl,
               @ref);
        }
        #endregion
    }

}