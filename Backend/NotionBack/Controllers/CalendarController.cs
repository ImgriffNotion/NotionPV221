using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.REST;
using NotionBack.Services.ConverterService;

namespace NotionBack.Controllers
{
    [Route("imgriff/calendars")]
    [ApiController]
    public class CalendarController(IUnitOfWork unitOfWork, IConvertService<CalendarDTO, Calendar> convertService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<CalendarDTO, Calendar> _convertService = convertService;


        [HttpGet]
        public async Task<IActionResult> Get(String id)
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/calendars?id={id}",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            try
            {

                var calendar = await _unitOfWork.Calendars.Get(new Guid(id));

                var _response = new RestResponse<Object>(200, _convertService.ToDTO(calendar), meta);
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
                uri = "/imgriff/calendars",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            var response = new RestResponse<string>(500, "Post method is empty", meta);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CalendarDTO calendarFromRequest)
        {
            var meta = new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/calendars",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            try
            {
                var calendar = await _unitOfWork.Calendars.Get(calendarFromRequest.Id);

                _unitOfWork.Calendars.Update(_convertService.FromDTO(calendarFromRequest));
                await _unitOfWork.Save();

                var _response = new RestResponse<Object>(200, calendarFromRequest, meta);
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
                uri = "/imgriff/calendars",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            var response = new RestResponse<string>(500, "Delete method is empty", meta);
            return Ok(response);
        }
    }
}
