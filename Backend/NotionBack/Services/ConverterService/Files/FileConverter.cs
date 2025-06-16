using Humanizer;
using NotionBack.DAL.Interfaces;
using NotionBack.Models.ModelsDTO;
using NotionBack.Services.FilesService;

namespace NotionBack.Services.ConverterService.Files
{
    public class FileConverter(IFileStorageService fileStorageService, IUnitOfWork unitOfWork) : IConvertService<FileDTO, DAL.Models.fileStructure.File>
    {
        private readonly IFileStorageService _fileStorageService = fileStorageService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<DAL.Models.fileStructure.File> FromDTO(FileDTO model)
        {
            if(model == null) 
                return new DAL.Models.fileStructure.File();

            var file = new DAL.Models.fileStructure.File()
            {
                Id = model.Id,
                Name = model.Name,
                Url = model.Url
            };

            return file;
        }

        public async Task<DAL.Models.fileStructure.File> FromDTO(DAL.Models.fileStructure.File domain, FileDTO dto)
        {
            if (domain == null || dto == null)
            {
                return new DAL.Models.fileStructure.File();
            }

            domain.Name = dto.Name;
            domain.Url = dto.Url;
            return domain;
        }

        public async Task<FileDTO> ToDTO(DAL.Models.fileStructure.File model)
        {
            if(model == null)
            {
                return new FileDTO();
            }

            var file = new FileDTO()
            {
                Id = model.Id,
                Name = model.Name,
                Url = model.Url
            };

            var tmp = await _fileStorageService.GetFileUrl(file);
            if (!String.IsNullOrEmpty(tmp))
            {
                file.Url = tmp;
                model.Url = file.Url;
                _unitOfWork.Files.Update(model);
                await _unitOfWork.Save();
            }


            return file;
        }
    }
}
