using Microsoft.EntityFrameworkCore;

namespace MapacenBackend.Entities;

public class MapacenDbContext : DbContext
{
    public MapacenDbContext(DbContextOptions<MapacenDbContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<County> Counties { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<SalesPoint> SalesPoints { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOffer> Favourites { get; set; }
    public DbSet<Likers> Likers { get; set; }
    public DbSet<Dislikers> Dislikers { get; set; }
    //public DbSet<ProductPicture> ProductPictures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Offer>()
            .HasOne(o => o.Product)
            .WithMany(p => p.Offers)
            .HasForeignKey(o => o.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserOffer>()
         .HasKey(uo => new { uo.OfferId, uo.UserId });
        modelBuilder.Entity<UserOffer>()
            .HasOne(uo => uo.User)
            .WithMany(u => u.Favourites)
            .HasForeignKey(uo => uo.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<UserOffer>()
            .HasOne(uo => uo.Offer)
            .WithMany(o => o.Favourites)
            .HasForeignKey(uo => uo.OfferId)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<County>()
            .HasMany(c => c.Addresses)
            .WithOne(a => a.County)
            .HasForeignKey(a => a.CountyId)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Offer)
            .WithMany(o => o.Comments)
            .HasForeignKey(c => c.OfferId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasMany(c => c.Likers)
            .WithOne(l => l.Comment)
            .HasForeignKey(l => l.CommentId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Comment>()
            .HasMany(c => c.Dislikers)
            .WithOne(l => l.Comment)
            .HasForeignKey(l => l.CommentId)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<Likers>()
            .HasKey(l => new { l.UserId, l.CommentId });
        modelBuilder.Entity<Likers>()
            .HasOne(l => l.User)
            .WithMany(u => u.Likers)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Likers>()
            .HasOne(l => l.Comment)
            .WithMany(c => c.Likers)
            .HasForeignKey(l => l.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Dislikers>()
            .HasKey(l => new { l.UserId, l.CommentId });
        modelBuilder.Entity<Dislikers>()
            .HasOne(l => l.User)
            .WithMany(u => u.Dislikers)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Dislikers>()
            .HasOne(l => l.Comment)
            .WithMany(c => c.Dislikers)
            .HasForeignKey(l => l.CommentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}