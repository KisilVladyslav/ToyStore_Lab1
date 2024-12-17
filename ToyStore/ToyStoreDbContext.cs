using Microsoft.EntityFrameworkCore;

public class ToyStoreDbContext : DbContext
{
    // Конструктор для ініціалізації контексту з параметрами
    public ToyStoreDbContext(DbContextOptions<ToyStoreDbContext> options)
        : base(options)
    { }

    // DbSet для кожної сутності
    public DbSet<Toy> Toys { get; set; }
    public DbSet<ToyCategory> ToyCategories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    // Налаштування зв'язків між таблицями
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Toy>()
            .HasOne(t => t.Category)
            .WithMany()
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Toy)
            .WithMany()
            .HasForeignKey(ci => ci.ToyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Toy)
            .WithMany()
            .HasForeignKey(oi => oi.ToyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
