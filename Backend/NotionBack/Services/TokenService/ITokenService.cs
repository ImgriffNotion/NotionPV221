namespace NotionBack.Services.TokenService
{
    public interface ITokenService<T>
    {
        public String GenerateToken(T tokenModel);
        public Task<T> CheckToken(String token);
    }
}
