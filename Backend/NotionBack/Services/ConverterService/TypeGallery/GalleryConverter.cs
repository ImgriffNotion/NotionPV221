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

        public Gallery FromDTO(Gallery domain, GalleryDTO dto)
        {
            domain.Title = dto.Title;

            if (dto.InternalContent != null && dto.InternalContent.Count != 0)
            {
                var tmpBuffer = new List<GalleryContent>();
                foreach (var dtoContent in dto.InternalContent)
                {
                    if (dtoContent.Id != null)
                    {
                        var domainContent = domain.Contents.Where(obj => obj.Id == dtoContent.Id).FirstOrDefault();
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
                    domain.Contents.Add(content);
                }
            }


            return domain;
        }

        public GalleryDTO ToDTO(Gallery model)
        {
            var gallery = new GalleryDTO()
            {
                Id = model.Id,
                Title = model.Title,
                ParentPageId = (Guid)model.ParentPageId,
                CreatedAt = model.CreatedAt,
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
