using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotionBack.DAL.Models
{
    public class Token
    {
        public Guid Id { get; set; }
        public DateTime? Iat { get; set; } = DateTime.Now;
        public DateTime? Exp { get; set; }
        public DateTime? DeleteDt { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
