using Microsoft.AspNetCore.Mvc;
using NotionBack.Models.Enums;
using NotionBack.REST;
using NotionBack.DAL.Interfaces;
using NotionBack.Services.ConverterService;
using NotionBack.DAL.Repositories;
using NotionBack.Models.ModelsDTO;
using NotionBack.DAL.Models;

namespace NotionBack.Controllers
{

    [ApiController]
    [Route("imgriff/pages")]
    public class PageController(IUnitOfWork unitOfWork, IConvertService<PageDTO, Page> convertService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<PageDTO, Page> _convertService = convertService;

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

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var meta = new RestMetaData()
            {
                method = "POST",
                name = "POST",
                uri = "/imgriff/page",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            try
            {
                var page = new PageDTO()
                {
                    Banner = "",
                    CreatedAt = DateTime.UtcNow,
                    Icon = "",
                    OwnerId = new Guid("719BEEFE-0F99-48AC-A3A1-08DD84246888"),
                    Slug = "kfsjdksld",
                    Title = "Notion 2025",
                    Type = new PageTypeDTO()
                    {
                        Name = PageType.Board.ToString(),
                        TypeCode = (int)PageType.Board
                    },
                };

                await _unitOfWork.Pages.Create(_convertService.FromDTO(page));
                await _unitOfWork.Save();
                var tmp1 = await _unitOfWork.Pages.GetAll();

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
