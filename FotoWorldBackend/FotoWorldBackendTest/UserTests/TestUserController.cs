using FotoWorldBackend.Controllers;
using FotoWorldBackend.Models;
using FotoWorldBackend.Models.UserModels;
using FotoWorldBackend.Services.Token;
using FotoWorldBackend.Services.UserS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace FotoWorldBackendTest.UserTests
{
    public class TestUserController
    {
        //https://www.c-sharpcorner.com/blogs/implementation-of-unit-test-using-xunit-and-moq-in-net-core-6-web-api
        public Mock<IUserService> mockUserService = new Mock<IUserService>();
       

        [Fact]
        public void test_get_offers_when_offers_exist()
        {
            var offersList = GetFakeOfferList();

            mockUserService.Setup(x => x.GetOfferList()).Returns(offersList);

            var userController = new UserController(mockUserService.Object);

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
            mockUserService.Setup(x => x.GetOfferList()).Returns(offerList);

            var userController = new UserController(mockUserService.Object);
            var result = userController.GetOffers();

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            
        }


        [Fact]
        public void test_detailed_view()
        {
            var offers=GetFakeOffers();
            mockUserService.Setup(x => x.GetOfferDetailed(1)).Returns(offers[1]);
            
            var userController = new UserController(mockUserService.Object);
            var result = userController.GetOfferDetailed(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<OfferWithPhotos>(okResult.Value);
            Assert.NotNull(response);
            Assert.Equal(offers[1].OfferId, response.OfferId);
        }

        [Fact]
        public void test_detailed_view_that_doesnt_exist()
        {
            var offers = GetFakeOffers();
            OfferWithPhotos doesntExist = null;
            mockUserService.Setup(x => x.GetOfferDetailed(5)).Returns(doesntExist);

            var userController = new UserController(mockUserService.Object);
            var result = userController.GetOfferDetailed(5);

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }





        [Fact]
        public void pokurwi_mnie()
        {
            mockUserService.Setup(x => x.FollowOffer(1, "tu_kurwa_musi_isc_valid_token")).Returns(true);

            var userController = new UserController(mockUserService.Object);
            var result = userController.FollowOffer(1);

            var okResult = Assert.IsType<OkResult>(result);


        }




        private List<OfferWithPhotos> GetFakeOffers()
        {
            List<OfferWithPhotos> data = new List<OfferWithPhotos> { 
                new OfferWithPhotos{ 
                    OperatorId= 1,
                    OperatorName="operator1",
                    Title="title1",
                    Description="description1",
                    PhotosId= new List<int>{1,2,3},
                    OperatorContact = new List<string>{"email1", "phonenumber1"}
               
                },
                new OfferWithPhotos{
                    OperatorId= 2,
                    OperatorName="operator2",
                    Title="title2",
                    Description="description2",
                    PhotosId= new List<int>{4,5,6},
                    OperatorContact = new List<string>{"email2", "phonenumber2"}

                },
                new OfferWithPhotos{
                    OperatorId= 3,
                    OperatorName="operator3",
                    Title="title3",
                    Description="description3",
                    PhotosId= new List<int>{7,8,9},
                    OperatorContact = new List<string>{"email3", "phonenumber3"}

                },
                new OfferWithPhotos{
                    OperatorId= 4,
                    OperatorName="operator4",
                    Title="title4",
                    Description="description4",
                    PhotosId= new List<int>{10,11,12},
                    OperatorContact = new List<string>{"email4", "phonenumber4"}

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