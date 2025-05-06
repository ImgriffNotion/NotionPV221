using NotionBack.Models.ModelsDTO;

namespace NotionBack.Services.ConverterService.Files
{
    public class FileConverter : IConvertService<FileDTO, DAL.Models.fileStructure.File>
    {
        public DAL.Models.fileStructure.File FromDTO(FileDTO model)
        {
            var file = new DAL.Models.fileStructure.File()
            {
                Id = model.Id,
                Name = model.Name,
                Url = model.Url,
            };

            return file;
        }

        public DAL.Models.fileStructure.File FromDTO(DAL.Models.fileStructure.File domain, FileDTO dto)
        {
            domain.Name = dto.Name;
            domain.Url = dto.Url;
            return domain;
        }

        public FileDTO ToDTO(DAL.Models.fileStructure.File model)
        {
            var file = new FileDTO()
            {
                Id = model.Id,
                Name = model.Name,
                Url = model.Url,
            };

            return file;
        }
    }
}
