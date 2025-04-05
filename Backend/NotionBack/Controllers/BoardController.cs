using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.DAL.Interfaces;

namespace NotionBack.Controllers
{
    [Route("imgriff/boards")]
    [ApiController]
    public class BoardController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public Task<IActionResult> Get(String id)
        {

            

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
