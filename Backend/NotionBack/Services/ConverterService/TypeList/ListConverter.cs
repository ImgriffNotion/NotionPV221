using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeList
{
    public class ListConverter(IConvertService<ListContentDTO, ListContent> content_converter) : IConvertService<ListDTO, DAL.Models.pageContents.List>
    {
        private readonly IConvertService<ListContentDTO, ListContent> _convertService = content_converter;


        public DAL.Models.pageContents.List FromDTO(ListDTO model)
        {
            var newList = new DAL.Models.pageContents.List()
            {
                Id = model.Id,
                ParentPageId = (Guid)model.ParentPageId,
                Title = model.Title,
                BoardId = (Guid)model.BoardId,
                Contents = new List<ListContent>()
            };

            if (model.InternalContent != null && model.InternalContent.Count != 0)
            {
                foreach (var content in model.InternalContent)
                {
                    newList.Contents.Add(_convertService.FromDTO(content));
                }
            }

            return newList;
        }

        public ListDTO ToDTO(DAL.Models.pageContents.List model)
        {
            var newList = new ListDTO()
            {
                Id = model.Id,
                ParentPageId = (Guid)model.ParentPageId,
                Title = model.Title,
                CreatedAt = model.CreatedAt,
                DeleteDt = model.DeleteDt,
                BoardId = (Guid)model.BoardId,
                InternalContent = new List<ListContentDTO>()
            };

            if (model.Contents != null && model.Contents.Count != 0)
            {
                foreach (var content in model.Contents)
                {
                    newList.InternalContent.Add(_convertService.ToDTO(content));
                }
            }

            return newList;
        }
    }
}
