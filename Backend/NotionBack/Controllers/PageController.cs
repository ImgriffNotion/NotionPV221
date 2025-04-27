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
                    OwnerId = new Guid("D22F06BD-15C0-448F-3B6C-08DD85027906"),
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
                var tmp1 = (await _unitOfWork.Pages.GetAll()).ToList();

                var pages = new List<PageDTO>();

                foreach(var tmpPage in tmp1)
                {
                    pages.Add(_convertService.ToDTO(tmpPage));  
                }

                var _response = new RestResponse<Object>(200, pages, meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(200, ex.Message, meta);
                return Ok(_response);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var meta = new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = "/imgriff/page",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            try
            {
                var pages = await _unitOfWork.Pages.GetAll();

                foreach (var page in pages)
                {
                    await _unitOfWork.Pages.Delete(page.Id);
                }
                await _unitOfWork.Save();

                var _response = new RestResponse<Object>(200, "Deleted success", meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(200, ex.Message, meta);
                return Ok(_response);
            }
        }

        [HttpDelete("delete-permanently")]
        public async Task<IActionResult> DeletePermanently()
        {
            var meta = new RestMetaData()
            {
                method = "DELETE",
                name = "DeletePermanently",
                uri = "/imgriff/page",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            try
            {
                var pages = await _unitOfWork.Pages.GetAll();

                foreach (var page in pages)
                {
                    await _unitOfWork.Pages.DeletePagePermanently(page);
                }
                await _unitOfWork.Save();

                var _response = new RestResponse<Object>(200, "Deleted success", meta);
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
