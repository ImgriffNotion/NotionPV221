using NotionBack.Models.OTP;
using StackExchange.Redis;
using System.Text.Json;

namespace NotionBack.Services.OTPService
{
    public class OtpService(IConnectionMultiplexer redis) : IOtpService
    {
        private readonly IDatabase _database = redis.GetDatabase();
        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        
        public async Task SaveOtp(string email, string otp, int ttlMinutes = 10)
        {
            try
            {
                var key = $"otp:{email}";
                var enty = new OTPModel
                {
                    otp = otp,
                    exp = DateTime.UtcNow.AddMinutes(ttlMinutes)
                };

                string json = JsonSerializer.Serialize(enty, _serializerOptions);
                await _database.StringSetAsync(key, json, TimeSpan.FromMinutes(ttlMinutes));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task<bool> VerifyOtp(string email, string otp)
        {
            var key = $"otp:{email}";
            string? json = await _database.StringGetAsync(key);
            if(string.IsNullOrEmpty(json)) return false;

            var entry = JsonSerializer.Deserialize<OTPModel>(json, _serializerOptions);
            if (entry == null || entry.exp < DateTime.UtcNow)
            {
                await _database.KeyDeleteAsync(key);
                return false;
            }
            return entry.otp == otp;
        }
    }
}
