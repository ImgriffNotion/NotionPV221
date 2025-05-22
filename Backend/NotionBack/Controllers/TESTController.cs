using Microsoft.AspNetCore.Mvc;
using NotionBack.REST;

namespace NotionBack.Controllers
{
    [Route("imgriff/testing")]
    [ApiController]
    public class TESTController(ILogger<TESTController> logger) : ControllerBase
    {
        private readonly ILogger<TESTController> _logger = logger;

        [HttpGet]
        public IActionResult TestMethod()
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "TestMethod",
                uri = "/imgriff/testing",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            _logger.LogInformation($"TestController mets: {meta.name} {meta.uri}");

            var _response = new RestResponse<String>(200, "YOU DID IT", meta);
            return Ok(_response);
        }
    }
}
