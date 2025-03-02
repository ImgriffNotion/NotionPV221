using Microsoft.EntityFrameworkCore;
using NotionBack.Db.Models;
using NotionBack.Db.Models.fileStructure;
using NotionBack.Db.Models.pageContents;
using NotionBack.Db.Models.pageContents.pageInPageContents;
using File = NotionBack.Db.Models.fileStructure.File;

public class NotionDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TypePage> TypePages { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<JustPageContent> JustPageContents { get; set; }
    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<GalleryContent> GalleryContents { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<TableContent> TableContents { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Calendar> Calendars { get; set; }
    public DbSet<CalendarContent> CalendarContents { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<List> Lists { get; set; }
    public DbSet<ListContent> ListContents { get; set; }

    public NotionDbContext(DbContextOptions<NotionDbContext> options)
        : base(options) => Database.EnsureCreated();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .ToTable("Users")
            .HasMany(u => u.Pages)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);

        // TypePage
        modelBuilder
            .Entity<TypePage>()
            .ToTable("PageTypes")
            .HasMany(tp => tp.Pages)
            .WithOne(p => p.Type)
            .HasForeignKey(p => p.TypeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Page


        // JustPageContent
        modelBuilder
            .Entity<JustPageContent>()
            .HasOne(jpc => jpc.ParentPage)
            .WithMany(p => p.JustPageContents)
            .HasForeignKey(jpc => jpc.ParentPageId)
            .OnDelete(DeleteBehavior.Cascade);

        // Gallery
        modelBuilder
            .Entity<Gallery>()
            .HasOne(g => g.ParentPage)
            .WithMany(p => p.Galleries)
            .HasForeignKey(g => g.ParentPageId)
            .OnDelete(DeleteBehavior.Cascade);

        // GalleryContent
        modelBuilder
            .Entity<GalleryContent>()
            .HasOne(gc => gc.Gallery)
            .WithMany(g => g.Contents)
            .HasForeignKey(gc => gc.GalleryId)
            .OnDelete(DeleteBehavior.Cascade);

        // Table
        modelBuilder
            .Entity<Table>()
            .HasOne(t => t.ParentPage)
            .WithMany(p => p.Tables)
            .HasForeignKey(t => t.ParentPageId)
            .OnDelete(DeleteBehavior.Cascade);

        // TableContent
        modelBuilder
            .Entity<TableContent>()
            .HasOne(tc => tc.Table)
            .WithMany(t => t.Contents)
            .HasForeignKey(tc => tc.TableId)
            .OnDelete(DeleteBehavior.Cascade);

        // Calendar
        modelBuilder
            .Entity<Calendar>()
            .HasOne(c => c.ParentPage)
            .WithMany(p => p.Calendars)
            .HasForeignKey(c => c.ParentPageId)
            .OnDelete(DeleteBehavior.Cascade);

        // CalendarContent
        modelBuilder
            .Entity<CalendarContent>()
            .HasOne(cc => cc.Calendar)
            .WithMany(c => c.Contents)
            .HasForeignKey(cc => cc.CalendarId)
            .OnDelete(DeleteBehavior.Cascade);

        // CalendarFile (many-to-many)
        modelBuilder.Entity<CalendarFile>().HasKey(cf => new { cf.FileId, cf.CalendarContentId });

        modelBuilder
            .Entity<CalendarFile>()
            .HasOne(cf => cf.File)
            .WithMany(f => f.CalendarFiles)
            .HasForeignKey(cf => cf.FileId);

        modelBuilder
            .Entity<CalendarFile>()
            .HasOne(cf => cf.CalendarContent)
            .WithMany(cc => cc.Files)
            .HasForeignKey(cf => cf.CalendarContentId);

        // Board
        modelBuilder
            .Entity<Board>()
            .HasOne(b => b.ParentPage)
            .WithMany(p => p.Boards)
            .HasForeignKey(b => b.ParentPageId)
            .OnDelete(DeleteBehavior.Cascade);

        // List
        modelBuilder
            .Entity<List>()
            .HasOne(l => l.ParentPage)
            .WithMany(p => p.Lists)
            .HasForeignKey(l => l.ParentPageId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<List>()
            .HasOne(l => l.Board)
            .WithMany(b => b.Lists)
            .HasForeignKey(l => l.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        // ListContent
        modelBuilder
            .Entity<ListContent>()
            .HasOne(lc => lc.List)
            .WithMany(l => l.Contents)
            .HasForeignKey(lc => lc.ListId)
            .OnDelete(DeleteBehavior.Cascade);

        // ListFile (many-to-many)
        modelBuilder.Entity<ListFile>().HasKey(lf => new { lf.FileId, lf.ListContentId });

        modelBuilder
            .Entity<ListFile>()
            .HasOne(lf => lf.File)
            .WithMany(f => f.ListFiles)
            .HasForeignKey(lf => lf.FileId);
        modelBuilder
            .Entity<ListFile>()
            .HasOne(lf => lf.ListContent)
            .WithMany(lc => lc.Files)
            .HasForeignKey(lf => lf.ListContentId);
    }
}
