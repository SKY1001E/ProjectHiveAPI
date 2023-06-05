using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectHiveAPI.DI;
using ProjectHiveAPI.Models;

namespace ProjectHiveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProject([FromBody] Project newProject)
        {
            var project = await _projectService.CreateProject(newProject);

            if (project != null)
            {
                return Ok(project); // Проект создан успешно
            }

            return BadRequest(); // Некорректный запрос или пользователь не найден
        }

        [HttpPost("{projectId}/add-user/{userId}")]
        public async Task<IActionResult> AddUserToProject(int projectId, int userId)
        {
            var result = await _projectService.AddUserToProject(projectId, userId);

            if (result)
            {
                return Ok(); // Пользователь добавлен к проекту успешно
            }

            return NotFound(); // Проект или пользователь не найден
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProjectsByUser(int userId)
        {
            var projects = await _projectService.GetProjectsByUser(userId);

            if (projects != null)
            {
                return Ok(projects); // Список проектов получен успешно
            }

            return NotFound(); // Пользователь не найден
        }
    }
}
