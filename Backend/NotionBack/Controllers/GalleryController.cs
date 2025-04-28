using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.REST;
using NotionBack.Services.ConverterService;

namespace NotionBack.Controllers
{
    [Route("imgriff/galleries")]
    [ApiController]
    public class GalleryController(IUnitOfWork unitOfWork, IConvertService<GalleryDTO, Gallery> convertService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<GalleryDTO, Gallery> _convertService = convertService;

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/galleries?id={id}",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            try
            {

                var gallery = await _unitOfWork.Galleries.Get(new Guid(id));

                var _response = new RestResponse<Object>(200, _convertService.ToDTO(gallery), meta);
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
                uri = "/imgriff/galleries",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            var response = new RestResponse<string>(500, "Post method is empty", meta);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] GalleryDTO galleryFromRequest)
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
                var gallery = await _unitOfWork.Galleries.Get(galleryFromRequest.Id);

                _unitOfWork.Galleries.Update(_convertService.FromDTO(galleryFromRequest));
                await _unitOfWork.Save();

                var _response = new RestResponse<Object>(200, galleryFromRequest, meta);
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
                uri = "/imgriff/galleries",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            var response = new RestResponse<string>(500, "Delete method is empty", meta);
            return Ok(response);
        }
    }
}
