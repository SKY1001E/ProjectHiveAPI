using Microsoft.EntityFrameworkCore;
using ProjectHiveAPI.DataBase;
using ProjectHiveAPI.DI;
using ProjectHiveAPI.Models;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace ProjectHiveAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ProjectHiveContext _context;

        public TaskService(ProjectHiveContext context) 
        {
            this._context = context;
        }

        public async Task<Models.Task?> CreateTask(Models.Task task)
        {
            task.Id = 0;
            var userExists = await _context.User.AnyAsync(u => u.Id == task.UserId);
            var projectExists = await _context.Project.AnyAsync(p => p.Id == task.ProjectId);
            var managerExists = await _context.User.AnyAsync(p => p.Id == task.ManagerId);
            if (!userExists || !projectExists || !managerExists)
                return null;

            _context.Task.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> UpdateTask(Models.Task task)
        {
            _context.Task.Update(task);
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            var task = await _context.Task.FindAsync(taskId);
            if (task == null)
                return false;

            _context.Task.Remove(task);
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Models.Task>> GetAllTasksByProjectAndUser(int userId, int projectId)
        {
            var tasks = await _context.Task
                .Where(t => t.UserId == userId && t.ProjectId == projectId)
                .ToListAsync();

            return tasks;
        }

    }
}
