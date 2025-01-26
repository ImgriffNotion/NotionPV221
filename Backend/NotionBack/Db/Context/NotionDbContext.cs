using Microsoft.EntityFrameworkCore;
using NotionBack.Db.Models;

public class NotionDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public NotionDbContext(DbContextOptions<NotionDbContext> options)
        : base(options) => Database.EnsureCreated();
}
