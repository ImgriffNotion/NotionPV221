using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.Models.ModelsDTO;
using NotionBack.REST;

namespace NotionBack.Controllers
{
    [Route("imgriff/testing")]
    [ApiController]
    public class TESTController : ControllerBase
    {
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

            var _response = new RestResponse<String>(200, "YOU DID IT Blyat", meta);
            return Ok(_response);
        }
    }
}
