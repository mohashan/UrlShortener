using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
//DbContext
{
    /// <summary>
    /// In case of add sql server provider in startup class
    /// </summary>
    /// <param name="options">DbContextOptions</param>
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
        
    }

    /// 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder); ==> it must execute first
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Url>().HasKey(x => x.Id);
        modelBuilder.Entity<Url>().Property(x => x.ShortUrl).IsRequired();
        modelBuilder.Entity<Url>().Property(x => x.LongUrl).IsRequired();
        modelBuilder.Entity<Url>().HasIndex(x => x.ShortUrl).IsUnique();
    }

    public DbSet<Url> Url { get; set; }
}
