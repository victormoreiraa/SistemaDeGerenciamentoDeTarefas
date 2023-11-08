using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDeGerenciamentoDeTarefas.Interfaces.Services;
using SistemaDeGerenciamentoDeTarefas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaDeGerenciamentoDeTarefas.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [Authorize(Policy = "RequireAuthenticatedUser")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksToDo>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasks();
            return Ok(new { message = "Todas as tarefas foram recuperadas com sucesso.", tasks });
        }
        [Authorize(Policy = "RequireAuthenticatedUser")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksToDo>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound(new { message = "Tarefa não encontrada." });
            }
            return Ok(new { message = "Tarefa recuperada com sucesso.", task });
        }
        [Authorize(Policy = "RequireAuthenticatedUser")]
        [HttpPost]
        public async Task<ActionResult<int>> CreateTask(TasksToDo task)
        {
            var taskId = await _taskService.CreateTask(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = taskId }, new { message = "Tarefa criada com sucesso.", taskId });
        }
        
        [Authorize(Policy = "RequireAuthenticatedUser")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TasksToDo task)
        {
            if (id != task.Id)
            {
                return BadRequest(new { message = "IDs de tarefa não coincidem." });
            }

            var result = await _taskService.UpdateTask(task);
            if (result)
            {
                return Ok(new { message = "Tarefa atualizada com sucesso." });
            }
            return NotFound(new { message = "Tarefa não encontrada." });
        }

        [Authorize(Policy = "RequireAuthenticatedUser")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteTask(id);
            if (result)
            {
                return Ok(new { message = "Tarefa excluída com sucesso." });
            }
            return NotFound(new { message = "Tarefa não encontrada." });
        }

        [Authorize(Policy = "RequireAuthenticatedUser")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] bool newStatus)
        {
            var success = await _taskService.UpdateTaskStatus(id, newStatus);
            if (success)
            {
                return Ok(new { message = "Status da tarefa atualizado com sucesso." });
            }
            return NotFound(new { message = "Tarefa não encontrada." });
        }
    }
}