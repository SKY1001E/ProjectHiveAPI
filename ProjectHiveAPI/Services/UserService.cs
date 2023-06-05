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

        async Task<User> IUserService.GetUserByEmail(string email)
        {
            var user = await _context.User.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

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

        async Task<User?> IUserService.GetUserById(int userId)
        {
            var user = await _context.User.FindAsync(userId);
            return user;
        }

        async Task<bool> IUserService.UpdateUser(User user)
        {
            var existingUser = await _context.User.FindAsync(user.Id);

            if (existingUser == null)
            {
                return false; // Пользователь не найден, обновление не выполнено
            }

            // Обновляем свойства существующего пользователя
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Role = user.Role;
            existingUser.Login = user.Login;
            existingUser.ProfileImage = user.ProfileImage;

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();

            return true; // Обновление выполнено успешно
        }
    }
}
