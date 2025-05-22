using Microsoft.AspNetCore.Mvc;
using NotionBack.REST;
using NotionBack.DAL.Interfaces;
using NotionBack.Services.ConverterService;
using NotionBack.Models.ModelsDTO;
using NotionBack.DAL.Models;
using NotionBack.Models;


namespace NotionBack.Controllers
{
   
    [ApiController]
    [Route("imgriff/person")]
    public class UserController(IUnitOfWork unitOfWork, IConvertService<UserDTO, User> userConverService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConvertService<UserDTO, User> _userConvertService = userConverService;

        [HttpGet]
        public async Task<IActionResult> Get(String id)
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = $"/imgriff/person?id={id}",
                locale = "en-US",
                serverTime = DateTime.UtcNow,
            };

            try
            {
                var user = await _unitOfWork.Users.Get(new Guid(id));
                var response = new RestResponse<object>(200, _userConvertService.ToDTO(user), meta);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new RestResponse<string>(500, ex.Message, meta);
                return Ok(response);
            }
        }

        [HttpPost]
        public IActionResult Post()
        {
            var meta = new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = $"/imgriff/person",
                locale = "en-US",
                serverTime = DateTime.UtcNow,
            };

            var response = new RestResponse<string>(418, "post method is empty", meta);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDTO userFromRequest)
        {
            var meta = new RestMetaData()
            {
                method = "PUT",
                name = "Put",
                uri = $"/imgriff/person",
                locale = "en-US",
                serverTime = DateTime.UtcNow,
            };

            try
            {
                var user = await _unitOfWork.Users.GetUserByEmail(userFromRequest.Email ?? "");

                _unitOfWork.Users.Update(await _userConvertService.FromDTO(userFromRequest));
                await _unitOfWork.Save();

                var response = new RestResponse<object>(200, userFromRequest, meta);
                return Ok(response);

            }
            catch (Exception)
            {
                var response = new RestResponse<string>(418, "post method is empty", meta);
                return Ok(response);

            }
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            var meta = new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = $"/imgriff/person",
                locale = "en-US",
                serverTime = DateTime.UtcNow,
            };
            var response = new RestResponse<string>(418, "Delete method is empty", meta);
            return Ok(response);
        }
    }
}
