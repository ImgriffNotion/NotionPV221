using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotionBack.DAL.Models;
using NotionBack.DAL.Models.fileStructure;
using NotionBack.DAL.Models.pageContents;
using NotionBack.DAL.Models.pageContents.pageInPageContents;
using NotionBack.DAL.Models.Templates;
using NotionBack.DAL.Models.Templates.pageContents;
using NotionBack.DAL.Models.Templates.pageContents.pageInPageContents;
using File = NotionBack.DAL.Models.fileStructure.File;

public class NotionDbContext : DbContext
{
    #region Data
    public DbSet<Token> Tokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<TypePage> TypePages { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<JustPageContent> JustPageContents { get; set; }
    public DbSet<Gallery> Gallerys { get; set; }
    public DbSet<GalleryContent> GalleryContents { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<TableContent> TableContents { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Calendar> Calendars { get; set; }
    public DbSet<CalendarContent> CalendarContents { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<List> Lists { get; set; }
    public DbSet<ListContent> ListContents { get; set; }
    #endregion

    #region Templates
    public DbSet<TypePageTemplate> TypePageTemplates { get; set; }
    public DbSet<Template> Templates { get; set; }
    public DbSet<JustPageContentTemplate> JustPageContentTemplates { get; set; }
    public DbSet<GalleryTemplate> GalleryTemplates { get; set; }
    public DbSet<GalleryContentTemplate> GalleryContentTemplates { get; set; }
    public DbSet<TableTemplate> TableTemplates { get; set; }
    public DbSet<TableContentTemplate> TableContentTemplates { get; set; }
    public DbSet<CalendarTemplate> CalendarTemplates { get; set; }
    public DbSet<CalendarContentTemplate> CalendarContentTemplates { get; set; }
    public DbSet<BoardTemplate> BoardTemplates { get; set; }
    public DbSet<ListTemplate> ListTemplates { get; set; }
    public DbSet<ListContentTemplate> ListContentTemplates { get; set; }
    #endregion

    public NotionDbContext(DbContextOptions<NotionDbContext> options)
        : base(options) { }

    public async Task TryInitializeDatabaseAsync(ILogger logger)
    {
        try
        {
            await Database.EnsureCreatedAsync(); // or EnsureCreatedAsync()
            logger.LogInformation("Database initialized.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to connect to the database.");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Data Settings

        //User
        modelBuilder
            .Entity<User>()
            .ToTable("Users")
            .HasMany(u => u.Pages)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder
            .Entity<User>()
            .ToTable("Users")
            .HasMany(u => u.Tokens)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.NoAction);

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
        #endregion

        #region Templates Settings

        // TypePageTemplate
        modelBuilder
            .Entity<TypePageTemplate>()
            .ToTable("PageTypesTemplate")
            .HasMany(tp => tp.Templates)
            .WithOne(p => p.TypeTemplate)
            .HasForeignKey(p => p.TypeTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // JustPageContentTemplate
        modelBuilder
            .Entity<JustPageContentTemplate>()
            .HasOne(jpc => jpc.Template)
            .WithMany(t => t.JustPageContentTemplates)
            .HasForeignKey(jpc => jpc.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // GalleryTemplate
        modelBuilder
            .Entity<GalleryTemplate>()
            .HasOne(g => g.Template)
            .WithMany(t => t.GalleryTemplates)
            .HasForeignKey(g => g.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // GalleryContentTemplate
        modelBuilder
            .Entity<GalleryContentTemplate>()
            .HasOne(gc => gc.GalleryTemplate)
            .WithMany(g => g.Contents)
            .HasForeignKey(gc => gc.GalleryTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // TableTemplate
        modelBuilder
            .Entity<TableTemplate>()
            .HasOne(tbl => tbl.Template)
            .WithMany(t => t.TableTemplates)
            .HasForeignKey(t => t.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // TableContentTemplate
        modelBuilder
            .Entity<TableContentTemplate>()
            .HasOne(tc => tc.TableTemplate)
            .WithMany(t => t.Contents)
            .HasForeignKey(tc => tc.TableTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // CalendarTemplate
        modelBuilder
            .Entity<CalendarTemplate>()
            .HasOne(c => c.Template)
            .WithMany(t => t.CalendarTemplates)
            .HasForeignKey(c => c.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // CalendarContentTemplate
        modelBuilder
            .Entity<CalendarContentTemplate>()
            .HasOne(cc => cc.CalendarTemplate)
            .WithMany(c => c.Contents)
            .HasForeignKey(cc => cc.CalendarTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // BoardTemplate
        modelBuilder
            .Entity<BoardTemplate>()
            .HasOne(b => b.Template)
            .WithMany(t => t.BoardTemplates)
            .HasForeignKey(b => b.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // ListTemplate
        modelBuilder
            .Entity<ListTemplate>()
            .HasOne(l => l.Template)
            .WithMany(t => t.ListTemplates)
            .HasForeignKey(l => l.TemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<ListTemplate>()
            .HasOne(l => l.BoardTemplate)
            .WithMany(b => b.ListTemplates)
            .HasForeignKey(l => l.BoardTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // ListContentTemplate
        modelBuilder
            .Entity<ListContentTemplate>()
            .HasOne(lc => lc.ListTemplate)
            .WithMany(l => l.Contents)
            .HasForeignKey(lc => lc.ListTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}
