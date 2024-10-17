using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Queries;
using StargateAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stargate_test
{
    [TestFixture]
    public class PersonControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<ILogger<PersonController>> _loggerMock;
        private PersonController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<PersonController>>();
            _controller = new PersonController(_mediatorMock.Object, _loggerMock.Object);


            var controllerContext = new ControllerContext();
            var httpContext = new DefaultHttpContext();
            controllerContext.HttpContext = httpContext;
            _controller.ControllerContext = controllerContext;
        }

        [Test]
        public async Task GetPeople_ReturnsOkResult_WhenSuccessful()
        {

            var expectedResult = new GetPeopleResult { Success = true, ResponseCode = 200 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPeople>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);


            var result = await _controller.GetPeople() as ObjectResult;


            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<GetPeopleResult>(result.Value);
        }

        [Test]
        public async Task GetPersonByName_ReturnsOkResult_WhenPersonFound()
        {

            var name = "John Doe";
            var expectedResult = new GetPersonByNameResult { Success = true, ResponseCode = 200, Person = new StargateAPI.Business.Dtos.PersonAstronaut() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPersonByName>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);


            var result = await _controller.GetPersonByName(name) as ObjectResult;


            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<GetPersonByNameResult>(result.Value);
        }

        [Test]
        public async Task CreatePerson_ReturnsCreatedResult_WhenSuccessful()
        {

            var name = "New Person";
            var expectedResult = new CreatePersonResult { Success = true, ResponseCode = 201 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPersonByName>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetPersonByNameResult { Person = null });
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePerson>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);


            var result = await _controller.CreatePerson(name) as ObjectResult;


            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.IsInstanceOf<CreatePersonResult>(result.Value);
        }

        [Test]
        public async Task CreatePerson_ReturnsBadRequest_WhenPersonAlreadyExists()
        {

            var name = "Existing Person";
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPersonByName>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetPersonByNameResult { Person = new StargateAPI.Business.Dtos.PersonAstronaut() });


            var result = await _controller.CreatePerson(name) as BadRequestObjectResult;


            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(result.Value);
        }
    }
}
