using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeGallery
{
    public class GalleryConverter(IConvertService<GalleryContentDTO, GalleryContent> convertService) : IConvertService<GalleryDTO, Gallery>
    {

        private readonly IConvertService<GalleryContentDTO, GalleryContent> _convertService = convertService;

        public Gallery FromDTO(GalleryDTO model)
        {
            var gallery = new Gallery()
            {
                Id = model.Id,
                Title = model.Title,
                ParentPageId = model.ParentPageId,
                DeleteDt = model.DeleteDt,
                Contents = new List<GalleryContent>()
            };

            if (model.InternalContent != null && model.InternalContent.Count != 0)
            {
                foreach (var content in model.InternalContent)
                {
                    gallery.Contents.Add(_convertService.FromDTO(content));
                }
            }

            return gallery;
        }

        public GalleryDTO ToDTO(Gallery model)
        {
            var gallery = new GalleryDTO()
            {
                Id = model.Id,
                Title = model.Title,
                ParentPageId = model.ParentPageId,
                DeleteDt = model.DeleteDt,
                InternalContent = new List<GalleryContentDTO>()
            };

            if (model.Contents != null && model.Contents.Count != 0)
            {
                foreach (var content in model.Contents)
                {
                    gallery.InternalContent.Add(_convertService.ToDTO(content));
                }
            }

            return gallery;
        }
    }
}
