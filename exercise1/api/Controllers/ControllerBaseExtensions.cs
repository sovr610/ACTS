using Microsoft.AspNetCore.Mvc;

namespace StargateAPI.Controllers
{
    public static class ControllerBaseExtensions
    {

        public static IActionResult GetResponse(this ControllerBase controllerBase, BaseResponse response, ILogger logger)
        {
            var httpResponse = new ObjectResult(response);
            httpResponse.StatusCode = response.ResponseCode;

            var path = controllerBase.Request.Path;
            var method = controllerBase.Request.Method;
            var Protocol = controllerBase.Request.Protocol;

            if (httpResponse.StatusCode != StatusCodes.Status500InternalServerError)
            {
                logger.LogInformation($"Request Performed: {method} {path} {Protocol} {response.ResponseCode}");
            }
            else
            {
                logger.LogError(response.Ex,$"ServerError Request: {method} {path} {Protocol} {response.ResponseCode}");
            }

            return httpResponse;
        }
    }
}