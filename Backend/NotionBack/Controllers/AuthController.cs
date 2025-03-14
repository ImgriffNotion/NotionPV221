
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

namespace NotionBack.Controllers
{
    [ApiController]
    [Route("imgriff/auth")]
    public class AuthController(IEmailService emailSender, IRandomService randomService, HttpClient client) : ControllerBase
    {
        private static Dictionary<string, OTPModel> OTPStore = new();
        private readonly HttpClient _httpClient = client;

        private readonly IEmailService emailService = emailSender;
        private readonly IRandomService _randomService = randomService;

        [HttpGet("")]
        public async Task<IActionResult> Get(String email)
        {

            var meta = new RestMetaData()
            {
                method = "GET",
                name = "Get",
                uri = "/imgriff/auth",
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

                var requestUrl = $"https://people.googleapis.com/v1/people/{body.Email}?personFields=names,emailAddresses,photos&key=AIzaSyBMEqsrKpqcK8bkBZ5GirxGuKnw7ORdcXY";
                var _response = await _httpClient.GetStringAsync(requestUrl);
                var userInfo = JsonSerializer.Deserialize<GoogleUserInfo>(_response);

                return Ok(userInfo);
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
            var authResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authResult.Succeeded) return BadRequest("Google authentication failed.");

            var email = authResult.Principal.FindFirstValue(ClaimTypes.Email);
            var name = authResult.Principal.FindFirstValue(ClaimTypes.Name);
            var picture = authResult.Principal.FindFirstValue("urn:google:picture");



            // Sign in the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.Name, name),
                new Claim("ProfilePicture", picture ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            Console.WriteLine(email);
            Console.WriteLine(name);
            Console.WriteLine(picture);

            return Redirect("/");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
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
