using ProjectHiveAPI.Models;
using Task = System.Threading.Tasks.Task;

namespace ProjectHiveAPI.DI
{
    public interface IProjectService
    {
        public Task<bool> AddUserToProject(int projectId, int userId);
        public Task<Project> CreateProject(Project newProject);
        public Task<IEnumerable<Project>> GetProjectsByUser(int userId);

    }
}
