using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>().Property
            (p => p.Description).HasMaxLength(500).IsRequired(false);

        builder.Entity<Product>().Property
            (p => p.Name).HasMaxLength(120).IsRequired();

        builder.Entity<Product>().Property
            (p => p.Code).HasMaxLength(20).IsRequired();
    }
}