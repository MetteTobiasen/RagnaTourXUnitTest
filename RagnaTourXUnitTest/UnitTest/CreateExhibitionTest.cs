using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RagnaTours;
using RagnaTours.Interfaces;
using RagnaTours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnaTourXUnitTest.UnitTest
{
    public class CreateExhibitionTest
    {

        [Fact]
        public void CreateExhibition_Post_ReturnsARedirectAndAddsExhibition_WhenModelStateIsValid()
        {
            //Arange
            var mockRepo = new Mock<IExhibitionRepository>();
            mockRepo.Setup(repo => repo.AddExhibition(It.IsAny<Exhibition>())).Verifiable();

            var @exhibition = new Exhibition() { Id = 1, Name = "test" };

            var createModel = new CreateExhibitionModel(mockRepo.Object);

            createModel.Exhibition = @exhibition;

            //Act
            var result = createModel.OnPost();

            //Assert 
            var redirectToActionResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("GetAllExhibitions", redirectToActionResult.PageName);
            mockRepo.Verify((e) => e.AddExhibition(@exhibition), Times.Once); 


        }
    }
}
