using MediatR;
using Microsoft.AspNetCore.Mvc;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Queries;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using StargateAPI.Business.Data;

namespace StargateAPI.Controllers
{
   
    /// <summary>
    /// Controller for managing Person-related operations.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PersonController> _logger;

        /// <summary>
        /// Initializes a new instance of the PersonController class.
        /// </summary>
        /// <param name="mediator">The mediator instance for handling requests.</param>
        public PersonController(IMediator mediator, ILogger<PersonController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of all people.
        /// </summary>
        /// <returns>A list of all people or an error message.</returns>
        /// <response code="200">Returns the list of people.</response>
        /// <response code="500">If an error occurs while processing the request.</response>
        [HttpGet("GetPeople")]
        [ResponseCache(Duration = 10)]
        [ProducesResponseType(typeof(IEnumerable<Person>), 200)]
        [ProducesResponseType(typeof(BaseResponse), 500)]
        public async Task<IActionResult> GetPeople()
        {
            try
            {
                var result = await _mediator.Send(new GetPeople()
                {

                });

                return this.GetResponse(result, _logger);
            }
            catch (Exception ex)
            {
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError,
                    Ex = ex
                }, _logger);
            }
        }

        /// <summary>
        /// Retrieves a person by their name.
        /// </summary>
        /// <param name="name">The name of the person to retrieve.</param>
        /// <returns>The person object or an error message.</returns>
        /// <response code="200">Returns the requested person.</response>
        /// <response code="404">If the person is not found.</response>
        /// <response code="500">If an error occurs while processing the request.</response>
        [HttpGet("GetPersonByName/{name}")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        [ProducesResponseType(typeof(BaseResponse), 500)]
        public async Task<IActionResult> GetPersonByName(string name)
        {
            try
            {
                var result = await _mediator.Send(new GetPersonByName()
                {
                    Name = name
                });

                return this.GetResponse(result, _logger);
            }
            catch (Exception ex)
            {
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError,
                    Ex = ex
                }, _logger);
            }
        }

        
        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="name">The name of the person to create.</param>
        /// <returns>The created person object or an error message.</returns>
        /// <response code="201">Returns the newly created person.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="500">If an error occurs while processing the request.</response>
        [HttpPost("CreatePerson")]
        [ProducesResponseType(typeof(Person), 201)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 500)]
        public async Task<IActionResult> CreatePerson([FromBody] string name)
        {
            try
            {
                var existingPerson = await _mediator.Send(new GetPersonByName { Name = name });
                if (existingPerson.Person != null)
                {
                    return BadRequest(new BaseResponse
                    {
                        Success = false,
                        Message = "A person with this name already exists.",
                        ResponseCode = (int)HttpStatusCode.BadRequest
                    });
                }

                var result = await _mediator.Send(new CreatePerson()
                {
                    Name = name
                });

                return this.GetResponse(result, _logger);
            }
            catch (Exception ex)
            {
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError,
                    Ex = ex
                }, _logger);
            }

        }
    }
}