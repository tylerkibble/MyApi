using Microsoft.EntityFrameworkCore;

// public class MyApiDbContext : DbContext
// {
//     public MyApiDbContext(DbContextOptions<MyApiDbContext> options)
//         : base(options)
//     {
//     }

//     public object Users { get; internal set; }
public class User
{
   public int Id { get; set; }
   public string Name { get; set; }
   public string Email { get; set; } 
}

public class MyApiDbContext : DbContext
{
    public MyApiDbContext(DbContextOptions<MyApiDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=myapi.db");
}
    
    //public DbSet<User> Users { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder options)
//         => options.UseSqlite("Data Source=myapi.db");
// }