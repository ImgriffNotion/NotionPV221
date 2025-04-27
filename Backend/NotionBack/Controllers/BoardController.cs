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
                uri = "/imgriff/board",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            try
            {
                var tmp = await _unitOfWork.Boards.GetAll();

                var _response = new RestResponse<Object>(200, tmp, meta);
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
                method = "GET",
                name = "GetAll",
                uri = "/imgriff/board",
                locale = "UK-UA",
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
                var _response = new RestResponse<Object>(200, ex.Message, meta);
                return Ok(_response);
            }
        }

        [HttpPut]
        public Task<IActionResult> Put()
        {
            return null;
        }

        [HttpDelete]
        public Task<IActionResult> Delete()
        {
            return null;
        }
    }
}
