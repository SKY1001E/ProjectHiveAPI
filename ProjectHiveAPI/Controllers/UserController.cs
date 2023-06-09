﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectHiveAPI.DI;
using ProjectHiveAPI.Models;

namespace ProjectHiveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        { 
            _userService = userService;
        }


        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _userService.GetUsers();

            return users;
        }

        [HttpGet("{email}")]
        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);

            return user;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);

            return NoContent();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest(); // Некорректный запрос, идентификатор не соответствует пользователю
            }

            var result = await _userService.UpdateUser(updatedUser);

            if (result)
            {
                return Ok(); // Обновление выполнено успешно
            }
            else
            {
                return NotFound(); // Пользователь не найден, обновление не выполнено
            }
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
