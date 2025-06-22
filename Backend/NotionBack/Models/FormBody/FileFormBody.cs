using Microsoft.AspNetCore.Mvc;
using NotionBack.Models.ModelsDTO;

namespace NotionBack.Models.FormBody
{
    public class FileFormBody
    {
        [FromForm]
        public IFormFile uploadedFile {  get; set; }
        [FromForm]
        public String? slug { get; set; }

    }
}
