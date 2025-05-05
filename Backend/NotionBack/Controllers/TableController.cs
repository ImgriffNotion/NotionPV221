using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.REST;
using NotionBack.Services.ConverterService;

namespace NotionBack.Controllers
{
    [Route("imgriff/tables")]
    [ApiController]
    public class TableController(IUnitOfWork unitOfWork, IConvertService<TableDTO, Table> convertService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<TableDTO, Table> _convertService = convertService;

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/tables?id={id}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            try
            {

                var table = await _unitOfWork.Tables.Get(new Guid(id));

                var _response = new RestResponse<Object>(200, _convertService.ToDTO(table), meta);
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
                uri = "/imgriff/tables",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            var response = new RestResponse<string>(500, "Post method is empty", meta);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TableDTO tableFromRequest)
        {
            var meta = new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/tables",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            try
            {
                var table = await _unitOfWork.Tables.Get(tableFromRequest.Id);

                _unitOfWork.Tables.Update(_convertService.FromDTO(tableFromRequest));
                await _unitOfWork.Save();

                var _response = new RestResponse<Object>(200, tableFromRequest, meta);
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
                uri = "/imgriff/tables",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            var response = new RestResponse<string>(500, "Delete method is empty", meta);
            return Ok(response);
        }
    }
}
