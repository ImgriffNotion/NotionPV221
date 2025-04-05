using Microsoft.AspNetCore.Mvc;
using NotionBack.Models.Enums;
using NotionBack.REST;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Repositories;

namespace NotionBack.Controllers
{

    [ApiController]
    [Route("imgriff/pages")]
    public class PageController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

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

            try
            {
                var tmp1 = await this._unitOfWork.Pages.GetAll();
                var _response = new RestResponse<Object>(200, tmp1, meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(200, ex.Message, meta);
                return Ok(_response);
            }

            

            
            
        }
    }
}
