using NotionBack.DAL.Models;
using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;

namespace NotionBack.Services.ConverterService.TypeBoard
{
    public class BoardConverter(IConvertService<ListDTO, DAL.Models.pageContents.List> convertService) : IConvertService<BoardDTO, Board>
    {
        private readonly IConvertService<ListDTO, DAL.Models.pageContents.List> _convertService = convertService;

        public async Task<Board> FromDTO(BoardDTO model)
        {
            if (model == null)
                return new Board();

            var board = new Board()
            {
                Title = model.Title,
                Lists = new List<DAL.Models.pageContents.List>()
            };

            if (model.InternalContent != null && model.InternalContent.Count != 0)
            {
                foreach (var content in model.InternalContent)
                {
                    board.Lists.Add(await _convertService.FromDTO(content));
                }
            }
            return board;
        }

        public async Task<Board> FromDTO(Board domain, BoardDTO dto)
        {
            if (domain == null || dto == null)
                return new Board();

            domain.Title = dto.Title;

            if (dto.InternalContent != null && dto.InternalContent.Count != 0)
            {

                var tmpBuffer = new List<List>();
                foreach (var dtoContent in dto.InternalContent)
                {
                    if (dtoContent.Id != null)
                    {
                        var domainContent = domain.Lists.Where(obj => obj.Id == dtoContent.Id).FirstOrDefault();
                        if (domainContent != null)
                        {
                            await _convertService.FromDTO(domainContent, dtoContent);
                        }
                    }
                    else
                    {
                        tmpBuffer.Add(await _convertService.FromDTO(dtoContent));
                    }
                }

                foreach (var content in tmpBuffer)
                {
                    domain.Lists.Add(content);
                }
            }

            return domain;
        }

        public async Task<BoardDTO> ToDTO(Board model)
        {
            if (model == null)
                return new BoardDTO();

            var board = new BoardDTO()
            {
                Id = model.Id,
                Title = model.Title,
                ParentPageId = model.ParentPageId,
                CreatedAt = model.CreatedAt,
                DeleteDt = model.DeleteDt,
                InternalContent = new List<ListDTO>()
            };

            if (model.Lists != null && model.Lists.Count != 0)
            {
                foreach (var content in model.Lists)
                {
                    board.InternalContent.Add(await _convertService.ToDTO(content));
                }
            }
            return board;
        }
    }
}
