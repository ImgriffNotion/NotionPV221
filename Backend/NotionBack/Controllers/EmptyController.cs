using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;
using NotionBack.REST;
using NotionBack.Services.ConverterService;

namespace NotionBack.Controllers
{
    [Route("imgriff/emptypage")]
    [ApiController]
    public class EmptyController(IUnitOfWork unitOfWork, IConvertService<EmptyPageContentDTO, JustPageContent> convertService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly IConvertService<EmptyPageContentDTO, JustPageContent> _convertService = convertService;

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/emptypage?id={id}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            try
            {

                var empty = await _unitOfWork.JustPageContents.Get(new Guid(id));

                var _response = new RestResponse<Object>(200, _convertService.ToDTO(empty), meta);
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
                uri = "/imgriff/emptypage",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            var response = new RestResponse<string>(500, "Post method is empty", meta);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EmptyPageContentDTO emptyFromRequest)
        {
            var meta = new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/emptypage",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            try
            {
                var empty = await _unitOfWork.Tables.Get(emptyFromRequest.Id);

                _unitOfWork.JustPageContents.Update(_convertService.FromDTO(emptyFromRequest));
                await _unitOfWork.Save();

                var _response = new RestResponse<Object>(200, emptyFromRequest, meta);
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
                uri = "/imgriff/emptypage",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            var response = new RestResponse<string>(500, "Delete method is empty", meta);
            return Ok(response);
        }
    }
}
