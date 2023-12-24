using Microsoft.EntityFrameworkCore;

public class MyApiDbContext : DbContext
{
    public MyApiDbContext(DbContextOptions<MyApiDbContext> options)
        : base(options)
    {
    }
    //public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=myapi.db");
}