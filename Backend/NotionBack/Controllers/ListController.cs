using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.REST;
using NotionBack.Services.ConverterService;

namespace NotionBack.Controllers
{
    [Route("imgriff/lists")]
    [ApiController]
    public class ListController(IUnitOfWork unitOfWork, IConvertService<ListDTO, DAL.Models.pageContents.List> convertService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<ListDTO, DAL.Models.pageContents.List> _convertService = convertService;

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/lists?id={id}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            try
            {

                var list = await _unitOfWork.Lists.Get(new Guid(id));

                var _response = new RestResponse<Object>(200, _convertService.ToDTO(list), meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(500, ex.Message, meta);
                return Ok(_response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var meta = new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = "/imgriff/lists",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            var response = new RestResponse<string>(500, "Post method is empty", meta);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ListDTO listFromRequest)
        {
            var meta = new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/lists",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            try
            {
                var list = await _unitOfWork.Lists.Get(listFromRequest.Id);

                _unitOfWork.Lists.Update(_convertService.FromDTO(listFromRequest));
                await _unitOfWork.Save();

                var _response = new RestResponse<Object>(200, listFromRequest, meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(500, ex.Message, meta);
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
                uri = "/imgriff/lists",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            var response = new RestResponse<string>(500, "Delete method is empty", meta);
            return Ok(response);
        }
    }
}
