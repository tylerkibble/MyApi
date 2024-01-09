using Microsoft.EntityFrameworkCore;
using MyApi.Models;


public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}

public class MyApiDbContext : DbContext
{

    public MyApiDbContext(DbContextOptions<MyApiDbContext> options)
        : base(options)
    {
    }
    public DbSet<UrlRedirect> UrlRedirects { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=myapi.db");
}