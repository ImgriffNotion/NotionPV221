using NotionBack.Models.FormBody;
using NotionBack.Models.ModelsDTO;

namespace NotionBack.Services.FilesService
{
    public interface IFileStorageService
    {
        public Task<String> GetFileUrl(FileDTO file);
        public Task<String> UploadFile(FileFormBody file);
        public Task DeleteFile(string path);

    }
}
