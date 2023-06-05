using ProjectHiveAPI.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectHiveAPI.DI
{
    public interface ITaskService
    {
        public Task<Models.Task> CreateTask(Models.Task task);
        public Task<bool> UpdateTask(Models.Task task);
        public Task<bool> DeleteTask(int userId);
        public Task<IEnumerable<Models.Task>> GetAllTasksByProjectAndUser(int userId, int projectId);
    }
}
