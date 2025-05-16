using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeGallery
{
    public class GalleryContentConverter : IConvertService<GalleryContentDTO, GalleryContent>
    {
        public async Task<GalleryContent> FromDTO(GalleryContentDTO model)
        {
            if (model == null)
                return new GalleryContent();


            var galleryContent = new GalleryContent()
            {
                Title = model.Title,
                Url = model.Url,
                Color = model.Color,
                Date = model.Date,
                Description = model.Description,
                Number = model.Number
            };

            return galleryContent;
        }

        public async Task<GalleryContent> FromDTO(GalleryContent domain, GalleryContentDTO dto)
        {
            if (domain == null || dto == null)
                return domain;

            domain.Title = dto.Title;
            domain.Url = dto.Url;
            domain.Color = dto.Color;
            domain.Date = dto.Date;
            domain.Description = dto.Description;
            domain.Number = dto.Number;

            return domain;
        }

        public async Task<GalleryContentDTO> ToDTO(GalleryContent model)
        {
            if (model == null)
                return new GalleryContentDTO();

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
