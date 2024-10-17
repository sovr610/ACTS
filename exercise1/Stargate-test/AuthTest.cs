using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Interfaces;
using StargateAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stargate_test
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<ILogger<AuthController>> _loggerMock;
        private AuthController _controller;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<AuthController>>();
            _controller = new AuthController(_userServiceMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Login_ReturnsOkResult_WhenLoginSuccessful()
        {

            var loginModel = new LoginModel { UserName = "testuser", Password = "testpassword" };
            var authResult = new AuthenticationResult { Success = true, Token = "test_token" };
            _userServiceMock.Setup(u => u.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(authResult);


            var result = await _controller.Login(loginModel) as OkObjectResult;


            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<AuthenticationResult>(result.Value);
        }

        [Test]
        public async Task Login_ReturnsBadRequest_WhenLoginFails()
        {

            var loginModel = new LoginModel { UserName = "testuser", Password = "wrongpassword" };
            var authResult = new AuthenticationResult { Success = false, Message = "Invalid credentials" };
            _userServiceMock.Setup(u => u.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(authResult);


            var result = await _controller.Login(loginModel) as BadRequestObjectResult;


            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOf<AuthenticationResult>(result.Value);
        }

        [Test]
        public async Task Register_ReturnsOkResult_WhenRegistrationSuccessful()
        {

            var registerModel = new RegisterModel { UserName = "newuser", Password = "newpassword", FirstName = "Pat", LastName = "Ben", Email = "Patt@acts.com" };
            var registrationResult = new UserRegistrationResult { Success = true, Token = "new_token" };
            _userServiceMock.Setup(u => u.RegisterUser(It.IsAny<RegisterModel>()))
                .ReturnsAsync(registrationResult);


            var result = await _controller.Register(registerModel) as OkObjectResult;


            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<UserRegistrationResult>(result.Value);
        }
    }
}
