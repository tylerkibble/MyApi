using Microsoft.EntityFrameworkCore;

public class MyApiContext : DbContext
{
    //public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=myapi.db");
}