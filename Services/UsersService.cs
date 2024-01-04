using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;


public class UsersService
{
    private readonly MyApiDbContext _context;

    public UsersService(MyApiDbContext context)
    {
        _context = context;
    }
    public async Task<User> CreateUser(User user)
    {
        _context.Users.Add(user);
        var result = await _context.SaveChangesAsync();
        Console.WriteLine($"SaveChangesAsync result: {result}");
        return user;
    }
    // public async Task<User> CreateUser(User user)
    // {
    //     //_context.Users.Add(user);
    //     await _context.SaveChangesAsync();
    //     Console.Write(user);
    //     return user;
    // }


    internal async Task<object?> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user;
        // throw new NotImplementedException();
    }
    public async Task<List<User>> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync();
        Console.WriteLine(users.ToArray());
        return users;
    }

    public async Task<bool> UpdateUser(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

}