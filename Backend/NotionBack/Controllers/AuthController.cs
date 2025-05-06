using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NotionBack.Services.EmailService;
using System.Security.Claims;
using NotionBack.Models.RequestsBody;
using NotionBack.Models.OTP;
using NotionBack.Services.RandomService;
using NotionBack.REST;
using NotionBack.DAL.Interfaces;
using NotionBack.Services.ConverterService;
using NotionBack.Models.ModelsDTO;
using NotionBack.DAL.Models;
using NotionBack.Models;
using NotionBack.Services.OTPService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using NotionBack.Models.Settings;
using Microsoft.Extensions.Options;
using NotionBack.Services.TokenService;

namespace NotionBack.Controllers
{
    [ApiController]
    [Route("imgriff/auth")]
    public class AuthController(
        IEmailService emailSender,
        IRandomService randomService,
        HttpClient client,
        IUnitOfWork unitOfWork,
        IOtpService otpService,
        IConvertService<UserDTO, User> userConvertService,
        ITokenService<TokenDTO> tokenService,
        IConvertService<TokenDTO, Token> tokenConvertService) : ControllerBase
    {
        private readonly HttpClient _httpClient = client;
        private static String redirectUrl = RedirectionURLs.localhostUrl;

        private readonly ITokenService<TokenDTO> _tokenService = tokenService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IEmailService emailService = emailSender;
        private readonly IOtpService _otpService = otpService;
        private readonly IRandomService _randomService = randomService;
        private readonly IConvertService<UserDTO, User> _userConvertService = userConvertService;
        private readonly IConvertService<TokenDTO, Token> _tokenConvertService = tokenConvertService;
        
        
        [HttpGet("get-otp")]
        public async Task<IActionResult> GetOtp(String email)
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "GetOtp",
                uri = $"/imgriff/auth/get-otp?email={email}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            if (string.IsNullOrWhiteSpace(email))
            {
                var response = new RestResponse<string>(400, "Email is required", meta);
                return Ok(response);
            }

            try
            {
                string verificationCode = _randomService.CreatorSymbolsByCount();
                await emailSender.SendEmail(email, verificationCode);
                await _otpService.SaveOtp(email, verificationCode);

                var response = new RestResponse<string>(200, "OTP has been sent", meta);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new RestResponse<string>(500, ex.Message, meta);
                return Ok(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PasscodeRequest body)
        {
            var meta = new RestMetaData()
            {
                method = "POST",
                name = "Post",
                uri = "/imgriff/auth",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            if (string.IsNullOrWhiteSpace(body.Email) || string.IsNullOrEmpty(body.Passcode))
            {
                var response = new RestResponse<Object>(400, "Email and passcode are required", meta);
                return Ok(response);
            }

            try
            {
                var isSuccessful = await _otpService.VerifyOtp(body.Email, body.Passcode);
                if (isSuccessful)
                {
                    var user = _userConvertService.ToDTO((await _unitOfWork.Users.GetUserByEmail(body.Email)));

                    var token = await GetJwtToken(user);

                    var response = new RestResponse<Object>(200, token, meta);
                    return Ok(response);

                }

                var _response = new RestResponse<Object>(400, "Otp is incorrect", meta);
                return Ok(_response);
            }
            catch (NullReferenceException ex)
            {
                var newUser = new UserDTO()
                {
                    Email = body.Email
                };
                await _unitOfWork.Users.Create(_userConvertService.FromDTO(newUser));
                await _unitOfWork.Save();
                newUser = _userConvertService.ToDTO(await _unitOfWork.Users.GetUserByEmail(newUser.Email));
                var token = await GetJwtToken(newUser);

                var _response = new RestResponse<Object>(200, token, meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<string>(500, ex.Message, meta);
                return Ok(_response);
            }

        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            try
            {
                var redirectUrl = Url.Action("GoogleResponse", "Auth");
                return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, GoogleDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Description = ex.Message
                });
            }
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "GoogleResponse",
                uri = "/imgriff/auth/google-response",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            var authResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authResult.Succeeded) return BadRequest("Google authentication failed.");


            var userFromResponse = getUserByResponse(authResult);

            // Sign in the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,userFromResponse.Email),
                new Claim(ClaimTypes.Name, userFromResponse.Name),
                new Claim(ClaimTypes.Surname, userFromResponse.Lastname),
                new Claim("ProfilePicture", userFromResponse.Avatar ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            try
            {
                var user = await _unitOfWork.Users.GetUserByEmail(userFromResponse.Email);
                user.Avatar = userFromResponse.Avatar;
                user.Name = userFromResponse.Name;
                user.Lastname = userFromResponse.Lastname;

                _unitOfWork.Users.Update(user);

            }
            catch (Exception ex)
            {
                await _unitOfWork.Users.Create(_userConvertService.FromDTO(userFromResponse));
            }

            await _unitOfWork.Save();
            return Redirect($"{redirectUrl}/login/success?email={userFromResponse.Email}");
        }

        [HttpGet("user-by-email")]
        public async Task<IActionResult> GetByEmail(String email)
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "GetByEmail",
                uri = $"/imgriff/auth/user-by-email?email={email}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            try
            {
                var user = await _unitOfWork.Users.GetUserByEmail(email);
                var token = await GetJwtToken(_userConvertService.ToDTO(user));
                var response = new RestResponse<Object>(200, token, meta);
                return Ok(response);
            }
            catch (NullReferenceException ex)
            {
                var _response = new RestResponse<Object>(400, "Email is incorrect", meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var response = new RestResponse<string>(500, ex.Message, meta);
                return Ok(response);
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "GetByEmail",
                uri = $"/imgriff/auth/logout",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var meta = new RestMetaData()
            {
                method = "DELETE",
                name = "Delete",
                uri = "/imgriff/auth",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            var _response = new RestResponse<string>(200, "Delete method is empty", meta);
            return Ok(_response);
        }

        private UserDTO getUserByResponse(AuthenticateResult authResult)
        {
            var fullname = authResult.Principal.FindFirstValue(ClaimTypes.Name).Split(" ");

            var user = new UserDTO();
            user.Email = authResult.Principal.FindFirstValue(ClaimTypes.Email);
            user.Avatar = authResult.Principal.FindFirstValue("urn:google:picture");
            user.Name = fullname[0];

            if (fullname.Length > 1)
            {
                user.Lastname = fullname[1];
            }

            return user;
        }

        private async Task<JwtTokenModel> GetJwtToken(UserDTO user)
        {
            //await JustMethod(user);
            var tokenDto = new TokenDTO()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Iat = DateTime.UtcNow,
                Exp = DateTime.UtcNow.AddHours(TokenValidTime.VaildTimeInHours),
                User = user
            };

            var token = _tokenService.GenerateToken(tokenDto);
            await _unitOfWork.Tokens.Create(_tokenConvertService.FromDTO(tokenDto));
            await _unitOfWork.Save();

            return new JwtTokenModel(){
                Jwt = token,
                User = user
            };
        }

        private async Task JustMethod(UserDTO user)
        {
            var tmp = await _unitOfWork.Tokens.GetAll();
            foreach (var token in tmp)
            {
                if (token.UserId == user.Id)
                    await _unitOfWork.Tokens.Delete(token.Id);
            }
            await _unitOfWork.Save();
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
    "birthday": "19854000-20",
    "photo": "https://avatar.mail.ru/user.jpg"
  }
}

Icloud
{
  "iss": "https://appleid.apple.com",
  "sub": "A1234567890abc1234de5678fghijk1234lm5678",
  "aud": "com.example.app",
  "exp": 1625134320,
  "iat": 1625130720,
  "nonce": "abcd1234xyz5678",
  "email": "user@example.com",
  "email_verified": true,
  "real_user_status": 0,
  "full_name": {
    "first_name": "John",
    "last_name": "Doe"
  }
}
 
 
 */
