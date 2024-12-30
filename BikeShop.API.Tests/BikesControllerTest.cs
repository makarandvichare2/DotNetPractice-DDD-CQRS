using Ardalis.Result;
using BikeShop.API.Controllers;
using BikeShop.UseCases.Bikes;
using BikeShop.UseCases.Bikes.Create;
using BikeShop.UseCases.Bikes.Get;
using BikeShop.UseCases.Bikes.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BikeShop.API.Tests
{
    public class BikesControllerTest
    {
        [Fact]
        public async Task GetListAsync_MustReturnOkObjectResult()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
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
            var bikeResult = Result.Success(bikes.AsEnumerable());
            mediator.Send(Arg.Any<ListBikeQuery>()).Returns(bikeResult);
            var controller = new BikesController(mediator);

            // Act
            var result = await controller.GetListAsync();

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<BikeListDTO>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetListAsync_MustReturnNotFound()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var bikes = new List<BikeListDTO> {
            };
            var bikeResult = Result.NotFound();
            mediator.Send(Arg.Any<ListBikeQuery>()).Returns(bikeResult);
            var controller = new BikesController(mediator);

            // Act
            var result = await controller.GetListAsync();

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async Task GetBikeDataAsync_MustReturnOkObjectResult()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var createBikeData = CreateBikeData();
            var bikeResult = Result.Success(createBikeData);
            mediator.Send(Arg.Any<GetBikeQuery>()).Returns(bikeResult);
            var controller = new BikesController(mediator);

            // Act
            var result = await controller.GetBikeDataAsync();

            // Assert
            var actionResult = Assert.IsType<ActionResult<CreateBikeDataDTO>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task CreateBike_MustCreatedResult()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var createBikeRequest = CreateBikeRequest();
            var bikeResult = Result.Success(1);
            mediator.Send(Arg.Any<CreateBikeCommand>()).Returns(bikeResult);
            var controller = new BikesController(mediator);

            // Act
            var result = await controller.CreateBike(createBikeRequest);

            // Assert
            var actionResult = Assert.IsType<CreatedResult>(result);
            Assert.IsType<CreatedResult>(actionResult);
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
        private CreateBikeDataDTO CreateBikeData()
        {
            CreateBikeDTO bike = CreateBikeDTO();
            var bikeModels = new List<LookUpDTO<int>>();
            var categories = new List<LookUpDTO<int>>();
            var manufacturers = new List<LookUpDTO<int>>();
            var colours = new List<string>();
            var currencies = new List<string>();
            var units = new List<string>();

            return new CreateBikeDataDTO(bike, bikeModels, categories, manufacturers, colours, currencies, units);
        }

        private CreateBikeDTO CreateBikeDTO()
        {
            return new CreateBikeDTO(1, 1, 1, 10, "£", "red", 44, "kg", "photo1.jpg");
        }

        private CreateBikeRequest CreateBikeRequest()
        {
            return new CreateBikeRequest(1, 1, 1, 10, "£", "red", 44, "kg", "photo1.jpg");
        }
        #endregion
    }
}