using System.Threading.Tasks;
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
        //_context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }


    internal Task<object?> GetUserById(int id)
    {
        throw new NotImplementedException();
    }
}
// public class UsersService
// {
//     public async Task<User> CreateUser(User user)
//     {
//         // Insert your logic for creating a user here.
//         // This might involve adding the user to a database and then returning the newly created user.

//         return user; // This is a placeholder. Replace with your actual implementation.
//     }
// }