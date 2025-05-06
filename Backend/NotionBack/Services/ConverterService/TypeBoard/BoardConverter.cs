using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;

namespace NotionBack.Services.ConverterService.TypeBoard
{
    public class BoardConverter(IConvertService<ListDTO, DAL.Models.pageContents.List> convertService) : IConvertService<BoardDTO, Board>
    {
        private readonly IConvertService<ListDTO, DAL.Models.pageContents.List> _convertService = convertService;

        public Board FromDTO(BoardDTO model)
        {
            var board = new Board()
            {
                Id = model.Id,
                Title = model.Title,
                ParentPageId = model.ParentPageId,
                Lists = new List<DAL.Models.pageContents.List>()
            };

            if (model.InternalContent != null && model.InternalContent.Count != 0)
            {
                foreach (var content in model.InternalContent)
                {
                    board.Lists.Add(_convertService.FromDTO(content));
                }
            }
            return board;
        }

        public Board FromDTO(Board domain, BoardDTO dto)
        {
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
                            _convertService.FromDTO(domainContent, dtoContent);
                        }
                    }
                    else
                    {
                        tmpBuffer.Add(_convertService.FromDTO(dtoContent));
                    }
                }

                foreach (var content in tmpBuffer)
                {
                    domain.Lists.Add(content);
                }
            }

            return domain;
        }

        public BoardDTO ToDTO(Board model)
        {

            var board = new BoardDTO()
            {
                Id = model.Id,
                Title = model.Title,
                ParentPageId = (Guid)model.ParentPageId,
                CreatedAt = model.CreatedAt,
                DeleteDt = model.DeleteDt,
                InternalContent = new List<ListDTO>()
            };

            if (model.Lists != null && model.Lists.Count != 0)
            {
                foreach (var content in model.Lists)
                {
                    board.InternalContent.Add(_convertService.ToDTO(content));
                }
            }
            return board;
        }
    }
}
