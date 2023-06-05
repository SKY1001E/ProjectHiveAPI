using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectHiveAPI.DI;
using ProjectHiveAPI.Models;
using ProjectHiveAPI.Services;

namespace ProjectHiveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Models.Task newTask)
        {
            try
            {
                var task = await _taskService.CreateTask(newTask);

                if (task != null)
                {
                    return Ok(task); // Проект создан успешно
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{taskId}")]
        public async Task<IActionResult> UpdateTask(int taskId, [FromBody] Models.Task task)
        {
            try
            {
                task.Id = taskId;

                // Обновление задачи через сервис
                var isUpdated = await _taskService.UpdateTask(task);

                if (isUpdated)
                    return Ok();

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            try
            {
                // Удаление задачи через сервис
                var isDeleted = await _taskService.DeleteTask(taskId);

                if (isDeleted)
                    return Ok();

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasksByProjectAndUser(int userId, int projectId)
        {
            try
            {
                // Получение всех задач для указанного пользователя и проекта через сервис
                var tasks = await _taskService.GetAllTasksByProjectAndUser(userId, projectId);

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
