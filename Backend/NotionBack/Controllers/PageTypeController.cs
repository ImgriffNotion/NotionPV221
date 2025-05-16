using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models;
using NotionBack.Models;
using NotionBack.Models.Enums;
using NotionBack.Models.ModelsDTO;
using NotionBack.REST;
using NotionBack.Services.ConverterService;

namespace NotionBack.Controllers
{
    [Route("imgriff/admin/page-type")]
    [ApiController]
    public class PageTypeController(IUnitOfWork unitOfWork, IConvertService<PageTypeDTO, TypePage> convertService) : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<PageTypeDTO, TypePage> _convertService = convertService;

        [HttpGet("register-type-page")]
        public async Task<IActionResult> RegTypeOfPage()
        {

            var meta = new RestMetaData()
            {
                method = "GET",
                name = "RegTypeOfPage",
                uri = "/imgriff/admin/page-type/register-type-page",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };
            meta._params["access_token"] = (JwtTokenModel)HttpContext.Items["jwt"];

            var listOfTypes = new List<PageTypeDTO>();
            foreach(var type in Enum.GetValues(typeof(PageType)))
            {
                try
                {
                    var isType = await _unitOfWork.PageTypes.GetTypePageByCode((int)type);
                }
                catch (Exception ex) 
                {
                    var newType = new PageTypeDTO()
                    {
                        Name = type.ToString(),
                        TypeCode = (int)type
                    };
                    listOfTypes.Add(newType);
                    await _unitOfWork.PageTypes.Create(await _convertService.FromDTO(newType));
                }
            }

            await _unitOfWork.Save();

            var _response = new RestResponse<Object>(200, listOfTypes, meta);
            return Ok(_response);
        }

        [HttpDelete("delete-type-page")]
        public async Task<IActionResult> DelTypeOfPage()
        {

            var meta = new RestMetaData()
            {
                method = "DELETE",
                name = "DelTypeOfPage",
                uri = "/imgriff/admin/page-type/delete-type-page",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };
            meta._params["access_token"] = (JwtTokenModel)HttpContext.Items["jwt"];

            var tmp = await _unitOfWork.PageTypes.GetAll();
            foreach (var type in tmp)
            {
                await _unitOfWork.PageTypes.Delete(type.Id);
            }

            await _unitOfWork.Save();

            var tmp1 = new List<PageTypeDTO>();
            foreach (var type in tmp)
            {
                tmp1.Add(await _convertService.ToDTO(type));
            }

            var _response = new RestResponse<Object>(200, tmp1, meta);
            return Ok(_response);
        }
    }
}
