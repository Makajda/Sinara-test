namespace SinaraApi.Common;
public class HrDbContext : DbContext {
    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseInMemoryDatabase("WorkDb");
    }
}
