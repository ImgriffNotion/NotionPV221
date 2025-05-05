using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.REST;
using NotionBack.Services.ConverterService;

namespace NotionBack.Controllers
{
    [Route("imgriff/boards")]
    [ApiController]
    public class BoardController(IUnitOfWork unitOfWork, IConvertService<BoardDTO, Board> converterService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<BoardDTO, Board> _convertService = converterService;

        [HttpGet]
        public async Task<IActionResult> Get(String id)
        {

            var meta = new RestMetaData()
            {
                method = "GET",
                name = "GetAll",
                uri = $"/imgriff/boards?id={id}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            try
            {
               
                var board = await _unitOfWork.Boards.Get(new Guid(id));

                var _response = new RestResponse<Object>(200, _convertService.ToDTO(board), meta);
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
                uri = "/imgriff/boards",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            var board = new BoardDTO()
            {
                CreatedAt = DateTime.Now,
                DeleteDt = DateTime.Now,
                Id = new Guid(),
                InternalContent = new List<ListDTO>(),
                ParentPageId = new Guid(),
                Title = "NEW BOARD FROM TODAY"
            };
            try
            {
                await _unitOfWork.Boards.Create(_convertService.FromDTO(board));
                await _unitOfWork.Save();
                var _response = new RestResponse<Object>(200, "OK", meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(500, ex.Message, meta);
                return Ok(_response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BoardDTO boardFromRequest)
        {
            var meta = new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = "/imgriff/boards",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            try
            {
                var board = await _unitOfWork.Boards.Get(boardFromRequest.Id);

                _unitOfWork.Boards.Update(_convertService.FromDTO(boardFromRequest));
                await _unitOfWork.Save();

                var _response = new RestResponse<Object>(200, boardFromRequest, meta);
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
                uri = "/imgriff/boards",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            var response = new RestResponse<string>(500, "Delete method is empty", meta);
            return Ok(response);
        }
    }
}
