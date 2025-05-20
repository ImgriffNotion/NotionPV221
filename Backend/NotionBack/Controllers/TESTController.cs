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
    public class TESTController : ControllerBase
    {
        [HttpGet]
        public void TestMethod()
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "TestMethod",
                uri = "/imgriff/testing",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            HttpContext.Response.WriteAsync($"YOU DID IT Blyat");
            return;

            //var _response = new RestResponse<String>(200, "YOU DID IT Blyat", meta);
            //return Ok(_response);
        }
    }
}
