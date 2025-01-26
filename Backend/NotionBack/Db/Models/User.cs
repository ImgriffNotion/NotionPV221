using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NotionBack.Db.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? AvatarPath { get; set; }
        public string? Dk { get; set; }
        public string? Salt { get; set; }
    }
}
