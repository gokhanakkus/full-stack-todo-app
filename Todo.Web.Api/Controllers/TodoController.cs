using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Todo.Data.Models;
using Todo.Services.Abstract;
using Todo.Services.Models.Todo;

namespace Todo.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        public TodoController(IDataRepository<TodoEntity> todoRepository)
        {
            TodoRepository = todoRepository;
        }

        public IDataRepository<TodoEntity> TodoRepository { get; }

        // add create todo endpoint
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoModel todo)
        {
            long userId = GetCurrentUserId();

            var todoEntity = new TodoEntity
            {
                Title = todo.Title,
                DueDate = todo.DueDate,
                UserId = userId,
                IsCompleted = false,
                IsDisabled = false
            };

            var savedEntity = await TodoRepository.AddAsync(todoEntity, userId);
            return Ok(savedEntity);
        }

        // add get user todos endpoint
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            long userId = GetCurrentUserId();

            var todos = await TodoRepository.GetAll().Where(x => x.UserId == userId).ToListAsync();

            return Ok(todos);
        }

        // update todo endpoint
        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] UpdateTodoModel updateTodoModel)
        {
            long userId = GetCurrentUserId();

            var dbTodo = await TodoRepository.GetByIdAsync(id);
            if (dbTodo == null)
            {
                return NotFound();
            }
            if (dbTodo.UserId != userId)
            {
                return Unauthorized();
            }

            dbTodo.Title = updateTodoModel.Title;
            dbTodo.DueDate = updateTodoModel.DueDate;

            var savedEntity = await TodoRepository.UpdateAsync(id, dbTodo, userId);
            return Ok(savedEntity);
        }

        // delete todo endpoint
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var userId = GetCurrentUserId();
            var dbTodo = await TodoRepository.GetByIdAsync(id);
            if (dbTodo == null)
            {
                return NotFound();
            }
            if (dbTodo.UserId != userId)
            {
                return Unauthorized();
            }

            await TodoRepository.DeleteAsync(id);
            return Ok();
        }

        // mark todo as done endpoint
        [HttpPut("{id}/done")]
        public async Task<IActionResult> Done([FromRoute] long id)
        {
            var userId = GetCurrentUserId();
            var dbTodo = await TodoRepository.GetByIdAsync(id);
            if (dbTodo == null)
            {
                return NotFound();
            }
            if (dbTodo.UserId != userId)
            {
                return Unauthorized();
            }

            dbTodo.IsCompleted = true;
            var savedEntity = await TodoRepository.UpdateAsync(id, dbTodo, userId);
            return Ok(savedEntity);
        }

        private long GetCurrentUserId()
        {
            var userIdStr = User.FindFirstValue("UserId");
            if (string.IsNullOrWhiteSpace(userIdStr))
            {
                throw new InvalidOperationException("User id not found");
            }

            return long.Parse(userIdStr);
        }
    }
}
