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
using Microsoft.AspNetCore.Http;
using System.Net.Http;

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
        private readonly IEmailService _emailService = emailSender;
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
                await _emailService.SendEmail(email, verificationCode);
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
        public async Task<IActionResult> ApproveUser([FromBody] PasscodeRequest body)
        {
            var meta = new RestMetaData()
            {
                method = "POST",
                name = "ApproveUser",
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
                    var user = await _userConvertService.ToDTO((await _unitOfWork.Users.GetUserByEmail(body.Email)));
                    var token = await GetJwtToken(user);
                    SetJwtToCookie(token.Jwt);
                    var response = new RestResponse<Object>(200, token, meta);
                    return Ok(response);

                }

                var _response = new RestResponse<Object>(400, "Otp is incorrect", meta);
                return Ok(_response);
            }
            catch (KeyNotFoundException)
            {
                var newUser = new UserDTO()
                {
                    Email = body.Email
                };
                await _unitOfWork.Users.Create(await _userConvertService.FromDTO(newUser));
                await _unitOfWork.Save();
                newUser = await _userConvertService.ToDTO(await _unitOfWork.Users.GetUserByEmail(newUser.Email));
                var token = await GetJwtToken(newUser);
                SetJwtToCookie(token.Jwt);
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
        public IActionResult LoginByGoogle()
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
                new Claim(ClaimTypes.Email,userFromResponse.Email ?? ""),
                new Claim(ClaimTypes.Name, userFromResponse.Name ?? ""),
                new Claim(ClaimTypes.Surname, userFromResponse.Lastname ?? ""),
                new Claim("ProfilePicture", userFromResponse.Avatar ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            try
            {
                if (userFromResponse.Email != null)
                {
                    var user = await _unitOfWork.Users.GetUserByEmail(userFromResponse.Email);
                    user.Avatar = userFromResponse.Avatar;
                    user.Name = userFromResponse.Name;
                    user.Lastname = userFromResponse.Lastname;

                    _unitOfWork.Users.Update(user);
                }
            }
            catch (Exception)
            {
                await _unitOfWork.Users.Create(await _userConvertService.FromDTO(userFromResponse));
            }

            await _unitOfWork.Save();
            return Redirect($"{redirectUrl}/login/success?email={userFromResponse.Email}");
        }

        [HttpGet("user-by-email")]
        public async Task<IActionResult> GetUserByEmail(String email)
        {
            var meta = new RestMetaData()
            {
                method = "GET",
                name = "GetUserByEmail",
                uri = $"/imgriff/auth/user-by-email?email={email}",
                locale = "en-US",
                serverTime = DateTime.UtcNow
            };

            try
            {
                var user = await _unitOfWork.Users.GetUserByEmail(email);
                var token = await GetJwtToken(await _userConvertService.ToDTO(user));
                var response = new RestResponse<Object>(200, token, meta);
                return Ok(response);
            }
            catch (NullReferenceException)
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

        private UserDTO getUserByResponse(AuthenticateResult authResult)
        {
            if (authResult == null || authResult.Principal == null)
            {
                return new UserDTO();
            }

            var fullname = authResult?.Principal.FindFirstValue(ClaimTypes.Name);
            String name = new("");
            String lastname = new("");

            if (fullname != null && !fullname.IsNullOrEmpty())
            {
                var tmp = fullname.Split(" ");
                name = tmp[0];
                if (tmp.Length > 1)
                {
                    lastname = tmp[1];
                }
            }


            var user = new UserDTO();
            user.Email = authResult?.Principal.FindFirstValue(ClaimTypes.Email);
            user.Avatar = authResult?.Principal.FindFirstValue("urn:google:picture");
            user.Name = name;
            user.Lastname = lastname;



            return user;
        }

        private async Task<JwtTokenModel> GetJwtToken(UserDTO user)
        {
            var tokenDto = new TokenDTO()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Iat = DateTime.UtcNow,
                Exp = DateTime.UtcNow.AddHours(TokenValidTime.VaildTimeInHours),
                User = user
            };

            var token = await _tokenService.GenerateToken(tokenDto);

            return new JwtTokenModel()
            {
                Jwt = token,
                User = user
            };
        }
        private void SetJwtToCookie(string jwt)
        {
            HttpContext.Response.Cookies.Append("token", jwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddHours(TokenValidTime.VaildTimeInHours)
            });
        }

    }
}
