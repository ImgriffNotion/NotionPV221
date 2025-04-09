using NotionBack.DAL.Models.pageContents;
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
                DeleteDt = model.DeleteDt,
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

        public BoardDTO ToDTO(Board model)
        {
            var board = new BoardDTO()
            {
                Id = model.Id,
                Title = model.Title,
                ParentPageId = (Guid)model.ParentPageId,
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
