using Dapper;
using Microsoft.Data.SqlClient;
using SistemaDeGerenciamentoDeTarefas.Interfaces.Repositories;
using SistemaDeGerenciamentoDeTarefas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class TaskRepository : ITaskRepository
{
    private readonly string _connectionString;

    public TaskRepository(IServiceProvider serviceProvider)
    {
        _connectionString = serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<TasksToDo>> GetAllTasks()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryAsync<TasksToDo>("SELECT * FROM TasksToDo");
        }
    }

    public async Task<TasksToDo> GetTaskById(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<TasksToDo>("SELECT * FROM TasksToDo WHERE Id = @Id", new { Id = id });
        }
    }

    public async Task<int> CreateTask(TasksToDo task)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var result = await connection.ExecuteScalarAsync<int>("INSERT INTO TasksToDo (Description, IsCompleted) VALUES (@Description, @IsCompleted); SELECT CAST(SCOPE_IDENTITY() as int)", task);
            return result;
        }
    }

    public async Task<bool> UpdateTask(TasksToDo task)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var affectedRows = await connection.ExecuteAsync("UPDATE TasksToDo SET Description = @Description, IsCompleted = @IsCompleted WHERE Id = @Id", task);
            return affectedRows > 0;
        }
    }

    public async Task<bool> DeleteTask(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var affectedRows = await connection.ExecuteAsync("DELETE FROM TasksToDo WHERE Id = @Id", new { Id = id });
            return affectedRows > 0;
        }
    }

    public async Task<bool> UpdateTaskStatus(int taskId, bool newStatus)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var parameters = new { TaskId = taskId, NewStatus = newStatus };
            var affectedRows = await connection.ExecuteAsync("UpdateTaskStatus", parameters, commandType: CommandType.StoredProcedure);
            return affectedRows > 0;
        }
    }
}
