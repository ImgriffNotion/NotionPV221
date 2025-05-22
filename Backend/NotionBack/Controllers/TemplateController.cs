using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotionBack.DAL.Interfaces;

namespace NotionBack.Controllers
{
    [Route("imgriff/templates")]
    [ApiController]
    public class TemplateController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public  IActionResult Get(string id)
        {
            return Ok("this method is empty");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("this method is empty");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("this method is empty");
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("this method is empty");
        }
    }
}
