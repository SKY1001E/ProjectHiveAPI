﻿using ProjectHiveAPI.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectHiveAPI.DI
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> AddUser(User user);
        public Task DeleteUser(int id);
    }
}