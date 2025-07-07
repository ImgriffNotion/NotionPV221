namespace NotionBack.Services.OTPService
{
    public interface IOtpService
    {
        public Task SaveOtp(String email, String otp, int ttlMinutes = 10);
        public Task<bool> VerifyOtp(String email, String otp);

        public Task RemoveOtp(String email);
    }
}
