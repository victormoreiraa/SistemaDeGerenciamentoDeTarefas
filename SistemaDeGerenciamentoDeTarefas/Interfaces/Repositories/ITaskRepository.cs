﻿using SistemaDeGerenciamentoDeTarefas.Models;

namespace SistemaDeGerenciamentoDeTarefas.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TasksToDo>> GetAllTasks();
        Task<TasksToDo> GetTaskById(int id);
        Task<int> CreateTask(TasksToDo task);
        Task<bool> UpdateTask(TasksToDo task);
        Task<bool> DeleteTask(int id);
        Task<bool> UpdateTaskStatus(int taskId, bool newStatus);
    }
}
