using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.pageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
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
        public Task<IActionResult> Get(String id)
        {

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var board = new BoardDTO()
            {
                CreatedAt = DateTime.Now,
                DeleteDt = DateTime.Now,
                Id = new Guid(),
                InternalContent = null,
                ParentPageId = new Guid(),
                Title = "NEW BOARD FROM TODAY"
            };

           await _unitOfWork.Boards.Create(_convertService.FromDTO(board));

            return null;
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
