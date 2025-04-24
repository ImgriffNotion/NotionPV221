namespace NotionBack.Models.ModelsDTO
{
    public class TokenDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Iat { get; set; }
        public DateTime Exp {  get; set; }

    }
}
