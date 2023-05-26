using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using RagnaTours.Interfaces;
using RagnaTours.Models;
using RagnaTours;

namespace RagnaTourXUnitTest.UnitTest
{
    public class GetAllExhibitionsTest
    {
        private readonly Mock<IExhibitionRepository> mockRepo;
        private readonly GetAllExhibitionsModel getAllExhibitionsModel;

        public GetAllExhibitionsTest()
        {
            mockRepo = new Mock<IExhibitionRepository>();
            getAllExhibitionsModel = new GetAllExhibitionsModel(mockRepo.Object);
        }

        [Fact]
        public void OnGet_ReturnsIActionResult_WithADictionaryOfExhibitions()
        {
            //Arange
            mockRepo.Setup(mockRepo => mockRepo.AllExhibition()).Returns(GetTestExhibitions());

            //Act
            var result = getAllExhibitionsModel.OnGet();
            Dictionary<int, Exhibition> myDictionary = getAllExhibitionsModel.Exhibitions;

            //Assert 
            Assert.IsAssignableFrom<IActionResult>(result);
            var viewResult = Assert.IsType<PageResult>(result);
            var actualMessages = Assert.IsType<Dictionary<int, Exhibition>>(myDictionary);
            Assert.Equal(2, myDictionary.Count);
            Assert.Equal("Fast Udstilling", myDictionary[1].Name);
            Assert.Equal("Roskilde Festival", myDictionary[2].Name);

        }

        private Dictionary<int, Exhibition> GetTestExhibitions()
        {
            var exhibitions = new Dictionary<int, Exhibition>
            {
                {
                    1, new Exhibition
                    {
                        Id = 1,
                        Name = "Fast Udstilling",
                        Description = "Test Test Test",
                        ImageName = "FastUdstilling"
                    }
                },
                {
                    2, new Exhibition
                    {
                        Id = 2,
                        Name = "Roskilde Festival",
                        Description = "Test Test Test",
                        ImageName = "RoskildeFestivalUdstilling"
                    }
                }
            };

            return exhibitions;
        }
    }
}