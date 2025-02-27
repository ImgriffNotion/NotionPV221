using Microsoft.AspNetCore.Mvc;

namespace NotionBack.Controllers
{
    [ApiController]
    [Route("api/imgriff/auth")]
    public class AuthController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get() {
            return null;
        }
    }
}

/*

gmail
{
  "iss": "https://accounts.google.com",
  "azp": "YOUR_CLIENT_ID",
  "aud": "YOUR_CLIENT_ID",
  "sub": "110169484474386276334",
  "email": "user@example.com",
  "email_verified": true,
  "name": "John Doe",
  "picture": "https://lh3.googleusercontent.com/a-/AOh14GgT.jpg",
  "given_name": "John",
  "family_name": "Doe",
  "iat": 1614324300,
  "exp": 1614327900
}

 
mail.ru
{
  "access_token": "abcdef123456...",
  "expires_in": 86400,
  "user": {
    "id": "123456789",
    "email": "user@mail.ru",
    "name": "Ivan Ivanov",
    "first_name": "Ivan",
    "last_name": "Ivanov",
    "gender": "male",
    "birthday": "1985-10-20",
    "photo": "https://avatar.mail.ru/user.jpg"
  }
}

 
 
 */