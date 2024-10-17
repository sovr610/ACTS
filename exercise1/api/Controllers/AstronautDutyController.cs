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
    /// Controller for managing Astronaut Duty operations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AstronautDutyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AstronautDutyController> _logger;

        /// <summary>
        /// Initializes a new instance of the AstronautDutyController class.
        /// </summary>
        /// <param name="mediator">The mediator instance for handling requests.</param>
        public AstronautDutyController(IMediator mediator, ILogger<AstronautDutyController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves astronaut duties by the astronaut's name.
        /// </summary>
        /// <param name="name">The name of the astronaut.</param>
        /// <returns>The list of duties for the specified astronaut or an error message.</returns>
        /// <response code="200">Returns the list of duties for the astronaut.</response>
        /// <response code="404">If the astronaut is not found.</response>
        /// <response code="500">If an error occurs while processing the request.</response>
        [HttpGet("GetAstronautDutiesByName/{name}")]
        [ProducesResponseType(typeof(IEnumerable<AstronautDuty>), 200)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        [ProducesResponseType(typeof(BaseResponse), 500)]
        public async Task<IActionResult> GetAstronautDutiesByName(string name)
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
        /// Creates a new astronaut duty.
        /// </summary>
        /// <param name="request">The create astronaut duty request model.</param>
        /// <returns>The created astronaut duty object or an error message.</returns>
        /// <response code="201">Returns the newly created astronaut duty.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="500">If an error occurs while processing the request.</response>
        [HttpPost("CreateAstronautDuty")]
        [ProducesResponseType(typeof(AstronautDuty), 201)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        [ProducesResponseType(typeof(BaseResponse), 500)]
        public async Task<IActionResult> CreateAstronautDuty([FromBody] CreateAstronautDuty request)
        {
            try
            {
                var result = await _mediator.Send(request);
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