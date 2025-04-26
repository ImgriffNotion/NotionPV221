
﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NotionBack.Services.EmailService;
using System.Security.Claims;
using NotionBack.Models.RequestsBody;
using NotionBack.Models.OTP;
using NotionBack.Services.RandomService;
using NotionBack.REST;
using System.Net.Http;
using System.Text.Json;
using NotionBack.Services.EmailAuthorizationService.EmailModels;
using NotionBack.DAL.Interfaces;
using NotionBack.Services.ConverterService;
using NotionBack.Models.ModelsDTO;
using NotionBack.DAL.Models;

namespace NotionBack.Controllers
{
    [ApiController]
    [Route("imgriff/auth")]
    public class AuthController(IEmailService emailSender, 
        IRandomService randomService, 
        HttpClient client,
        IUnitOfWork unitOfWork,
        IConvertService<UserDTO, User> userConvertService) : ControllerBase
    {
        private static Dictionary<string, OTPModel> OTPStore = new();
        private static Dictionary<string, UserDTO> UserDTOStore = new();
        private readonly HttpClient _httpClient = client;


        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IEmailService emailService = emailSender;
        private readonly IRandomService _randomService = randomService;
        private readonly IConvertService<UserDTO, User> _userConvertService = userConvertService;

        [HttpGet("get-otp")]
        public async Task<IActionResult> GetOtp(String email)
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "GetOtp",
                uri = "/imgriff/auth/get-otp",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            try

            {
                string verificationCode = _randomService.CreatorOnePassCodeByRandom();
                await emailSender.SendEmail(email, verificationCode);

                OTPStore.Add(email, new OTPModel()
                {
                    user_email = email,
                    otp = verificationCode,
                    expired = DateTime.UtcNow.AddMinutes(10)
                });

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
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            try
            {

                if (!OTPStore.ContainsKey(body.Email))
                {
                    var response = new RestResponse<string>(400, "user should have made request to get the OTP", meta);
                    return Ok(response);
                }

                OTPModel otpModel = OTPStore[body.Email];

                if (otpModel == null || !otpModel.otp.Equals(body.Passcode) || otpModel.expired < DateTime.UtcNow)
                {
                    var response = new RestResponse<string>(400, "OTP is incorrect", meta);
                    return Ok(response);
                }

                OTPStore.Remove(body.Email);

                var user = new UserDTO()
                {
                    Email = body.Email
                };

                await _unitOfWork.Users.Create(_userConvertService.FromDTO(user));
                await _unitOfWork.Save();

                var _response = new RestResponse<Object>(200, user, meta);
                return Ok();
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<string>(500, ex.Message, meta);
                return Ok(_response);
            }



        }

        [HttpPost("get-access-token")]
        public async Task<IActionResult> GetAccessToken()
        {
            var tokenRequest = new Dictionary<string, string>
            {
                { "client_id", "24881042872-ep2a4i7maue9ecm09f0viigeuvperr5t.apps.googleusercontent.com" },
                { "client_secret", "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" },
                { "grant_type", "client_credentials" },
                { "scope", "openid email profile" }
            };

            var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token",
                new FormUrlEncodedContent(tokenRequest));

            var responseString = await response.Content.ReadAsStringAsync();
            return Ok(responseString);
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
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            var authResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authResult.Succeeded) return BadRequest("Google authentication failed.");


            var user = getUserByResponse(authResult);

            // Sign in the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Lastname),
                new Claim("ProfilePicture", user.Avatar ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            await _unitOfWork.Users.Create(_userConvertService.FromDTO(user));
            await _unitOfWork.Save();

            UserDTOStore[user.Email] = user;

            return Redirect($"http://localhost:3000/login/success?email={user.Email}");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
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
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            //CCA8C249-5947-45AF-A3A2-08DD84246888
            try
            {
                await _unitOfWork.Users.Delete(new Guid("CCA8C249-5947-45AF-A3A2-08DD84246888"));
                await _unitOfWork.Save();
                var _response = new RestResponse<Object>(200, "CCA8C249-5947-45AF-A3A2-08DD84246888", meta);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                var _response = new RestResponse<Object>(500, ex.Message, meta);
                return Ok(_response);
            }
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

        [HttpGet("user-by-email")]
        public async Task<IActionResult> GetByEmail(String email)
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "GetByEmail",
                uri = "/imgriff/auth/user-by-email",
                locale = "UK-UA",
                serverTime = DateTime.UtcNow
            };

            try
            {
                var response = new RestResponse<Object>(200, UserDTOStore[email], meta);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new RestResponse<string>(500, ex.Message, meta);
                return Ok(response);
            }
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
