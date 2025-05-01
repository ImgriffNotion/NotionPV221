using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeGallery
{
    public class GalleryContentConverter : IConvertService<GalleryContentDTO, GalleryContent>
    {
        public GalleryContent FromDTO(GalleryContentDTO model)
        {
            var galleryContent = new GalleryContent()
            {
                Id = model.Id,
                Title = model.Title,
                Url = model.Url,
                GalleryId = model.GalleryId,
                Color = model.Color,
                Date = model.Date,
                Description = model.Description,
                Number = model.Number
            };

            return galleryContent;
        }

        public GalleryContentDTO ToDTO(GalleryContent model)
        {
            var galleryContent = new GalleryContentDTO()
            {
                Id = model.Id,
                Title = model.Title,
                Url = model.Url,
                GalleryId = (Guid)model.GalleryId,
                Color = model.Color,
                Date = model.Date,
                Description = model.Description,
                Number = model.Number
            };

            return galleryContent;
        }
    }
}
