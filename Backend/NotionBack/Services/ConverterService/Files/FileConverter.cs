using Humanizer;
using NotionBack.Models.ModelsDTO;
using NotionBack.Services.FilesService;

namespace NotionBack.Services.ConverterService.Files
{
    public class FileConverter(IFileStorageService fileStorageService) : IConvertService<FileDTO, DAL.Models.fileStructure.File>
    {
        private readonly IFileStorageService _fileStorageService = fileStorageService;

        public async Task<DAL.Models.fileStructure.File> FromDTO(FileDTO model)
        {

            var file = new DAL.Models.fileStructure.File()
            {
                Id = model.Id,
            };

            if(model.uploadedFile != null)
                file.Name = model.uploadedFile.FileName;

            file.Url = await _fileStorageService.UploadFile(model);

            return file;
        }

        public async Task<DAL.Models.fileStructure.File> FromDTO(DAL.Models.fileStructure.File domain, FileDTO dto)
        {
            if (dto.uploadedFile != null)
            {
                domain.Name = dto.uploadedFile.FileName;
                domain.Url = await _fileStorageService.UploadFile(dto);
            }
            else
            {
                domain.Url = await _fileStorageService.GetFileUrl(dto);
            }
            return domain;
        }

        public async Task<FileDTO> ToDTO(DAL.Models.fileStructure.File model)
        {
            var file = new FileDTO()
            {
                Id = model.Id,
                Name = model.Name,
            };

            file.Url = await _fileStorageService.GetFileUrl(file);

            return file;
        }
    }
}
