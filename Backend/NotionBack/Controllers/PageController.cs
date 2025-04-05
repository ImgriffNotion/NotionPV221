using Microsoft.AspNetCore.Mvc;
using NotionBack.Models.Enums;
using NotionBack.REST;

namespace NotionBack.Controllers
{

    [ApiController]
    [Route("imgriff/pages")]
    public class PageController() : ControllerBase
    {
        //private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "GetAll",
                uri = "/imgriff/page",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            var tmp = new String("");

            foreach(var value in Enum.GetValues(typeof(PageType)))
            {
                Console.WriteLine((int)value);
            }    

            //var tmp = await this._unitOfWork.Pages.GetAll();

            var _response = new RestResponse<Object>(200, tmp, meta);
            return Ok(_response);
        }
    }
}
