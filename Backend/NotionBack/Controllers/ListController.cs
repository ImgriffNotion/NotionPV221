using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.DAL.Interfaces;
using NotionBack.Models.ModelsDTO.ContentDTO;
using NotionBack.Services.ConverterService;

namespace NotionBack.Controllers
{
    [Route("imgriff/lists")]
    [ApiController]
    public class ListController(IUnitOfWork unitOfWork, IConvertService<ListDTO, DAL.Models.pageContents.List> convertService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<ListDTO, DAL.Models.pageContents.List> _convertService = convertService;

        [HttpGet]
        public async Task<IActionResult> Get(String id)
        {
            DAL.Models.pageContents.List list = await _unitOfWork.Lists.Get(new Guid(id));
            ListDTO listDTO = _convertService.ToDTO(list);
            return null;
        }

        [HttpPost]
        public Task<IActionResult> Post()
        {
            return null;
        }

        [HttpPut]
        public Task<IActionResult> Put()
        {
            return null;
        }

        [HttpDelete]
        public Task<IActionResult> Delete()
        {
            return null;
        }
    }
}
