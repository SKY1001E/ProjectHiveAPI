using Microsoft.EntityFrameworkCore;
using ProjectHiveAPI.DataBase;
using ProjectHiveAPI.DI;
using ProjectHiveAPI.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectHiveAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ProjectHiveContext _context;

        public UserService(ProjectHiveContext context) 
        {
            this._context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }

        async Task<User> IUserService.AddUser(User user)
        {
            this._context.User.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        async Task IUserService.DeleteUser(int id)
        {
            var user = await this._context.User.FindAsync(id);

            if (user == null)
            {
                return;
            }

            _context.User.Remove(user);

            await _context.SaveChangesAsync();
        }
    }
}
