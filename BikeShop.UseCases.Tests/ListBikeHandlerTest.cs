using BikeShop.UseCases.Bikes.List;
using FluentAssertions;
using NSubstitute;

namespace BikeShop.UseCases.Tests
{
    public class ListBikeHandlerTest
    {
        private readonly IListBikeQueryService _service = Substitute.For<IListBikeQueryService>();
        private ListBikeHandler _handler;

        public ListBikeHandlerTest()
        {
            _handler = new ListBikeHandler(_service);
        }
        [Fact]
        public async Task Handle_WithValidInput_ReturnBikes()
        {
            //Arrange
            var query = new ListBikeQuery();
            var bikesData = BikesData();

            var bikes = new List<BikeListDTO> { 
                new BikeListDTO(
                   bikesData[0].manufacturer,
                   bikesData[0].@ref,
                   bikesData[0].bikeModel,
                   bikesData[0].category,
                   bikesData[0].price,
                   bikesData[0].weight,
                   bikesData[0].colour,
                   bikesData[0].imgUrl,
                    0),
                new BikeListDTO(
                   bikesData[1].manufacturer,
                   bikesData[1].@ref,
                   bikesData[1].bikeModel,
                   bikesData[1].category,
                   bikesData[1].price,
                   bikesData[1].weight,
                   bikesData[1].colour,
                   bikesData[1].imgUrl,
                    1)
            };

            _service.ListAsync().Returns(Task.FromResult(bikes.AsEnumerable()));

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            result.Value.Should().BeEquivalentTo(bikes);
        }

        #region private methods
        private List<(string manufacturer,
            Guid @ref,
            string bikeModel,
            string category, 
            string price,
            string weight,
            string colour, 
            string imgUrl)> BikesData()
        {
            var list = new List<(string manufacturer,
            Guid @ref,
            string bikeModel,
            string category,
            string price,
            string weight,
            string colour,
            string imgUrl)>();
            list.Add(
                ("manufacturer 1",
                 Guid.NewGuid(),
                 "bikeModel 1",
                 "category 1",
                "£25.44",
                "455kg",
                "Red",
                "/asset/photo1.jpg")
                );
            list.Add(
                ("manufacturer 2",
                 Guid.NewGuid(),
                 "bikeModel 2",
                 "category 2",
                "£245.44",
                "855kg",
                "Blue",
                "/asset/photo2.jpg")
                );

            return list;
        }
        #endregion
    }
}