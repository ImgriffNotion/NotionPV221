namespace NotionBack.Models.OTP
{
    public class OTPModel
    {
        public String user_email {  get; set; }
        public String otp {  get; set; }
        public DateTime expired { get; set; }
    }
}
