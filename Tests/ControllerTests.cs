using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using WebUI.Controllers;
using StoreBL;
using Models;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace Tests
{
    public class ControllerTests
    {
        [Fact]
        public void StoreControllerIndexShouldReturnListOfStores()
        {
            //ARRANGE
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.GetAllStores()).Returns(
                new List<StoreFront>()
                {
                    new StoreFront()
                    {
                        Id = 1,
                        Name = "SLS1",
                        Address = "123 City"
                    },
                    new StoreFront()
                    {
                        Id = 2,
                        Name = "SLS2",
                        Address = "123 State"
                    }
                });

            var controller = new StoreController(mockBL.Object);

            //ACT
            var result = controller.Index();

            //ASSERT
            //First, make sure we are getting the right type of result obj
            var viewResult = Assert.IsType<ViewResult>(result);
            //Next, we want to make sure, that in this view result, the model we have for it 
            //is list of StoreVM
            var model = Assert.IsAssignableFrom<IEnumerable<StoreVM>>(viewResult.ViewData.Model);
            //Lastly, make sure there's two of them
            Assert.Equal(2, model.Count());
        }
    }
}
