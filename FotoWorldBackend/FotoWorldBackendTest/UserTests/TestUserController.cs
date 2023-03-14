using FotoWorldBackend.Controllers;
using FotoWorldBackend.Models;
using FotoWorldBackend.Models.UserModels;
using FotoWorldBackend.Services.UserS;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FotoWorldBackendTest.UserTests
{
    public class TestUserController
    {
        //https://www.c-sharpcorner.com/blogs/implementation-of-unit-test-using-xunit-and-moq-in-net-core-6-web-api
        public Mock<IUserService> mock = new Mock<IUserService>();

        [Fact]
        public void test_get_offers_when_offers_exist()
        {
            var offersList = GetFakeOfferList();

            mock.Setup(x => x.GetOfferList()).Returns(offersList);

            var userController = new UserController(mock.Object);

            var result = userController.GetOffers();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<List<OfferListObject>>(okResult.Value);
            Assert.NotNull(response);
            Assert.Equal(GetFakeOfferList().Count(), response.Count);
            
        }

        [Fact]
        public void test_get_offers_when_offers_dont_exist()
        {
            List<OfferListObject> offerList = null;
            mock.Setup(x => x.GetOfferList()).Returns(offerList);

            var userController = new UserController(mock.Object);
            var result = userController.GetOffers();

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            
        }




        private List<Offer> GetFakeOffers()
        {
            List<Offer> data = new List<Offer> { 
                new Offer{ 
                    Id = 1,
                    Description= "description1",
                    Title= "title1",
                    OperatorId= 1,
               
                },
                new Offer
                {
                    Id= 2,
                    Description = "description2",
                    Title = "title2",
                    OperatorId= 2,
                },
                new Offer{
                    Id = 3,
                    Description= "description3",
                    Title= "title3",
                    OperatorId= 3,

                },
                new Offer
                {
                    Id= 4,
                    Description = "description4",
                    Title = "title4",
                    OperatorId= 4,
                }



            };

            return data;
        }




        private List<OfferListObject> GetFakeOfferList()
        {
            List<OfferListObject> data = new List<OfferListObject> {
                new OfferListObject{
                    OfferId= 1,
                    OperatorName="operator1",
                    Title="title1"

                },
                new OfferListObject
                {
                    OfferId= 2,
                    OperatorName="operator2",
                    Title="title2"
                },
                new OfferListObject{
                    OfferId= 3,
                    OperatorName="operator3",
                    Title="title3"

                },
                new OfferListObject
                {
                    OfferId= 4,
                    OperatorName="operator4",
                    Title="titl41"
                }



            };

            return data;
        }
    }
}