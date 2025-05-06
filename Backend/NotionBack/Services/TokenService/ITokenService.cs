namespace NotionBack.Services.TokenService
{
    public interface ITokenService<T>
    {
        public String GenerateToken(T tokenModel);
        public Task<bool> CheckToken(String token);
    }
}
