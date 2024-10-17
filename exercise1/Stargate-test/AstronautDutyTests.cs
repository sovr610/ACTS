using Moq;
using StargateAPI.Business.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StargateAPI.Business.Queries;
using StargateAPI.Controllers;
using Microsoft.AspNetCore.Http;

namespace Stargate_test
{
    [TestFixture]
    public class AstronautDutyControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<ILogger<AstronautDutyController>> _loggerMock;
        private AstronautDutyController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<AstronautDutyController>>();
            _controller = new AstronautDutyController(_mediatorMock.Object, _loggerMock.Object);

            // Mock ControllerBase context
            var controllerContext = new ControllerContext();
            var httpContext = new DefaultHttpContext();
            controllerContext.HttpContext = httpContext;
            _controller.ControllerContext = controllerContext;
        }

        [Test]
        public async Task GetAstronautDutiesByName_ReturnsOkResult_WhenSuccessful()
        {

            var name = "John Doe";
            var expectedResult = new GetPersonByNameResult { Success = true, ResponseCode = 200 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPersonByName>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);


            var result = await _controller.GetAstronautDutiesByName(name) as ObjectResult;


            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<GetPersonByNameResult>(result.Value);
        }

        [Test]
        public async Task CreateAstronautDuty_ReturnsCreatedResult_WhenSuccessful()
        {

            var request = new CreateAstronautDuty { Name = "John Doe", Rank = "Captain", DutyTitle = "Mission Commander" };
            var expectedResult = new CreateAstronautDutyResult { Success = true, ResponseCode = 201 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateAstronautDuty>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);


            var result = await _controller.CreateAstronautDuty(request) as ObjectResult;


            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.IsInstanceOf<CreateAstronautDutyResult>(result.Value);
        }
    }
}
