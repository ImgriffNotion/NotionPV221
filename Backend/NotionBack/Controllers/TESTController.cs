using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.Models.ModelsDTO;
using NotionBack.REST;
using System.IO;
using System.Net.Http;

namespace NotionBack.Controllers
{
    [Route("imgriff/testing")]
    [ApiController]
    public class TESTController(ILogger logger) : ControllerBase
    {
        private readonly ILogger _logger = logger;

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
