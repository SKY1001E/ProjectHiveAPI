using Microsoft.AspNetCore.Mvc;
using ProjectHiveAPI.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectHiveAPI.DI
{
    public interface IAuthService
    {
        public Task<User> Login(AuthModel login);

        public Task Register(User user);
    }
}
