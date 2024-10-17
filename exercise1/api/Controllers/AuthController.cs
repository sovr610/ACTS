using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Interfaces;
using StargateAPI.Business.Services;

namespace StargateAPI.Controllers
{
    /// <summary>
    /// Controller responsible for handling authentication-related operations.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="userService">The user service for handling authentication and registration operations.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates a user and returns a token.
        /// </summary>
        /// <param name="model">The login model containing user credentials.</param>
        /// <returns>An authentication token if login is successful, or an error message.</returns>
        /// <response code="200">Returns the authentication token if login is successful.</response>
        /// <response code="400">If the login credentials are invalid or an error occurs during authentication.</response>
        /// <response code="500">If an unexpected error occurs while processing the request.</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticationResult), 200)]
        [ProducesResponseType(typeof(AuthenticationResult), 400)]
        [ProducesResponseType(typeof(AuthenticationResult), 500)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _userService.AuthenticateUser(model.UserName, model.Password);
            if (token.Success)
            {
                return Ok(token);
            }
            else
            {
                return BadRequest(token);
            }
        }

        /// <summary>
        /// Registers a new user and returns a token.
        /// </summary>
        /// <param name="model">The registration model containing user information.</param>
        /// <returns>An authentication token if registration is successful, or an error message.</returns>
        /// <response code="200">Returns the authentication token if registration is successful.</response>
        /// <response code="400">If the registration information is invalid or an error occurs during registration.</response>
        /// <response code="500">If an unexpected error occurs while processing the request.</response>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserRegistrationResult), 200)]
        [ProducesResponseType(typeof(UserRegistrationResult), 400)]
        [ProducesResponseType(typeof(UserRegistrationResult), 500)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var token = await _userService.RegisterUser(model);
            if (token.Success)
            {
                return Ok(token);
            }
            else
            {
                return BadRequest(token);
            }
        }

    }
}
