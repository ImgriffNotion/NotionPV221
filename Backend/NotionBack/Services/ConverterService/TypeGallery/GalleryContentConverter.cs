using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.fileStructure;
using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.Models.ModelsDTO;
using NotionBack.Models.ModelsDTO.ContentDTO.InternalContentDTO;

namespace NotionBack.Services.ConverterService.TypeGallery
{
    public class GalleryContentConverter(IUnitOfWork unitOfWork, IConvertService<FileDTO, NotionBack.DAL.Models.fileStructure.File> fileConvertService) : IConvertService<GalleryContentDTO, GalleryContent>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<FileDTO, NotionBack.DAL.Models.fileStructure.File> _fileConvertService = fileConvertService;
        public async Task<GalleryContent> FromDTO(GalleryContentDTO model)
        {
            if (model == null)
                return new GalleryContent();


            var galleryContent = new GalleryContent()
            {
                Title = model.Title,
                Color = model.Color,
                Date = model.Date,
                Description = model.Description,
                Number = model.Number
            };

            if(model.file != null)
            {
                galleryContent.Url = model.file.Id.ToString();
            }

            return galleryContent;
        }

        public async Task<GalleryContent> FromDTO(GalleryContent domain, GalleryContentDTO dto)
        {
            if (domain == null || dto == null)
                return new GalleryContent();

            domain.Title = dto.Title;
            domain.Color = dto.Color;
            domain.Date = dto.Date;
            domain.Description = dto.Description;
            domain.Number = dto.Number;

            if (dto.file != null)
            {
                domain.Url = dto.file.Id.ToString();
            }

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
                GalleryId = model.GalleryId,
                Color = model.Color,
                Date = model.Date,
                Description = model.Description,
                Number = model.Number
            };

            if (!String.IsNullOrEmpty(model.Url))
            {
                galleryContent.file = await _fileConvertService.ToDTO(await _unitOfWork.Files.Get(new Guid(model.Url)));
            }

            return galleryContent;
        }
    }
}
