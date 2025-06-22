using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.DAL.Interfaces;
using NotionBack.DAL.Models.fileStructure;
using NotionBack.Models.FormBody;
using NotionBack.Models.ModelsDTO;
using NotionBack.REST;
using NotionBack.Services.ConverterService;
using NotionBack.Services.FilesService;
using NotionBack.Services.PageContent;

namespace NotionBack.Controllers
{
    [Route("imgriff/files")]
    [ApiController]
    public class FileController(IConvertService<FileDTO, NotionBack.DAL.Models.fileStructure.File> fileConverter,
        IFileStorageService fileStorageService,
        IPageContentService pageContentService,
        IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IConvertService<FileDTO, NotionBack.DAL.Models.fileStructure.File> _fileConverter = fileConverter;
        private readonly IFileStorageService _fileStorageService = fileStorageService;
        private readonly IPageContentService _pageContentService = pageContentService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpPost("user-files")]
        public async Task<IActionResult> UserFiles([FromForm] FileFormBody body)
        {
            var meta = new RestMetaData()
            {
                method = "POST",
                name = "UserFiles",
                uri = $"/imgriff/files/user-files",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            if(body == null || body.uploadedFile == null)
            {
                var response = new RestResponse<string>(400, "Model is invalid", meta);
                return Ok(response);
            }    

            var file = new FileDTO();
            file.Id = Guid.NewGuid();
            file.Name = body.uploadedFile.FileName;
            file.Url = await _fileStorageService.UploadFile(body);
            await _unitOfWork.Files.Create(await _fileConverter.FromDTO(file));
            await _unitOfWork.Save();

            var savedFile = await _unitOfWork.Files.Get(file.Id);

            var _response = new RestResponse<FileDTO>(200, file, meta);
            return Ok(_response);
        }

        [HttpPost("gallery-images")]
        public async Task<IActionResult> GalleryImages([FromForm] FileFormBody body)
        {
            var meta = new RestMetaData()
            {
                method = "POST",
                name = "GalleryImages",
                uri = $"/imgriff/files/user-files",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            if (body == null || body.uploadedFile == null || String.IsNullOrEmpty(body.slug))
            {
                var response = new RestResponse<string>(400, "Model is invalid", meta);
                return Ok(response);
            }

            var imageUrl = await _fileStorageService.UploadFile(body);


            return Ok(imageUrl);
        }


    }
}
