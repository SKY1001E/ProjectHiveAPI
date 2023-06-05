using Microsoft.EntityFrameworkCore;
using ProjectHiveAPI.DataBase;
using ProjectHiveAPI.DI;
using ProjectHiveAPI.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectHiveAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectHiveContext _context;

        public ProjectService(ProjectHiveContext context)
        {
            this._context = context;
        }

        public async Task<bool> AddUserToProject(int projectId, int userId)
        {
            var project = await _context.Project.FindAsync(projectId);
            var user = await _context.User.FindAsync(userId);

            if (project == null || user == null)
            {
                return false; // Проект или пользователь не найден, добавление не выполнено
            }

            // Добавляем запись в связующую таблицу или обновляем существующую
            var existingRecord = await _context.UserProject.FirstOrDefaultAsync(pu => pu.ProjectId == projectId && pu.UserId == userId);

            if (existingRecord == null)
            {
                _context.UserProject.Add(new UserProject { ProjectId = projectId, UserId = userId, DateAdded = DateTime.Now});
            }
            else
            {
                existingRecord.UserId = userId;
                existingRecord.ProjectId = projectId;
                existingRecord.DateAdded = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            return true; // Добавление выполнено успешно
        }

        public async Task<Project> CreateProject(Project newProject)
        {
            newProject.Id = 0;
            var user = await _context.User.FindAsync(newProject.UserId);

            if (user == null)
            {
                return null; // Пользователь не найден, создание проекта не выполнено
            }
            
            _context.Project.Add(newProject);

            await _context.SaveChangesAsync();
            // Добавляем запись в связующую таблицу
            _context.UserProject.Add(new UserProject { ProjectId = newProject.Id, UserId = user.Id, DateAdded = DateTime.Now });

            await _context.SaveChangesAsync();

            return newProject;
        }
        
        public async Task<Project?> GetProjectById(int projectId)
        {
            var project = await _context.Project.FindAsync(projectId);
            return project;
        }

        public async Task<IEnumerable<Project>> GetProjectsByUser(int userId)
        {
            var projectIds = await _context.UserProject
                .Where(pu => pu.UserId == userId)
                .Select(pu => pu.ProjectId)
                .ToListAsync();

            var projects = await _context.Project
                .Where(p => projectIds.Contains(p.Id))
                .ToListAsync();

            return projects;
        }
    }
}
