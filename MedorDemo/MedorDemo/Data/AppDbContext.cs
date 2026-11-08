using Microsoft.EntityFrameworkCore;

namespace MedorDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<ClientCategory> ClientCategories => Set<ClientCategory>();
    public DbSet<ClientProductStock> ClientProductStocks => Set<ClientProductStock>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasMany(product => product.Categories)
            .WithMany(category => category.Products)
            .UsingEntity<ProductCategory>(
                right => right
                    .HasOne(productCategory => productCategory.Category)
                    .WithMany()
                    .HasForeignKey(productCategory => productCategory.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade),
                left => left
                    .HasOne(productCategory => productCategory.Product)
                    .WithMany()
                    .HasForeignKey(productCategory => productCategory.ProductId)
                    .OnDelete(DeleteBehavior.Cascade),
                join =>
                {
                    join.HasKey(productCategory => new
                    {
                        productCategory.ProductId,
                        productCategory.CategoryId
                    });
                });

        modelBuilder.Entity<Client>()
            .HasMany(client => client.Categories)
            .WithMany(category => category.Clients)
            .UsingEntity<ClientCategory>(
                right => right
                    .HasOne(clientCategory => clientCategory.Category)
                    .WithMany()
                    .HasForeignKey(clientCategory => clientCategory.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade),
                left => left
                    .HasOne(clientCategory => clientCategory.Client)
                    .WithMany()
                    .HasForeignKey(clientCategory => clientCategory.ClientId)
                    .OnDelete(DeleteBehavior.Cascade),
                join =>
                {
                    join.HasKey(clientCategory => new
                    {
                        clientCategory.ClientId,
                        clientCategory.CategoryId
                    });
                });

        // unique constraints
        modelBuilder.Entity<Product>()
            .HasIndex(product => product.Name)
            .IsUnique();

        modelBuilder.Entity<Client>()
            .HasIndex(client => client.Name)
            .IsUnique();

        modelBuilder.Entity<Category>()
            .HasIndex(category => category.Name)
            .IsUnique();


        modelBuilder.Entity<ClientProductStock>()
            .HasKey(stock => new
            {
                stock.ClientId,
                stock.ProductId
            });

        modelBuilder.Entity<ClientProductStock>()
            .HasOne(stock => stock.Client)
            .WithMany(client => client.ProductStocks)
            .HasForeignKey(stock => stock.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ClientProductStock>()
            .HasOne(stock => stock.Product)
            .WithMany(product => product.ClientStocks)
            .HasForeignKey(stock => stock.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ClientProductStock>()
            .Property(stock => stock.Quantity)
            .IsRequired();
    }
}