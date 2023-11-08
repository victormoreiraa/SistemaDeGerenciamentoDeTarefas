using SistemaDeGerenciamentoDeTarefas.Interfaces.Repositories;
using SistemaDeGerenciamentoDeTarefas.Interfaces.Services;
using SistemaDeGerenciamentoDeTarefas.Models;

namespace SistemaDeGerenciamentoDeTarefas.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TasksToDo>> GetAllTasks()
        {
            return await _taskRepository.GetAllTasks();
        }

        public async Task<TasksToDo> GetTaskById(int id)
        {
            return await _taskRepository.GetTaskById(id);
        }

        public async Task<int> CreateTask(TasksToDo task)
        {
            return await _taskRepository.CreateTask(task);
        }

        public async Task<bool> UpdateTask(TasksToDo task)
        {
            return await _taskRepository.UpdateTask(task);
        }

        public async Task<bool> DeleteTask(int id)
        {
            return await _taskRepository.DeleteTask(id);
        }

        public async Task<bool> UpdateTaskStatus(int taskId, bool newStatus)
        {
            return await _taskRepository.UpdateTaskStatus(taskId, newStatus);
        }

    }
}
