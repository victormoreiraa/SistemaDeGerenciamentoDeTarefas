using Moq;
using SistemaDeGerenciamentoDeTarefas.Interfaces.Repositories;
using SistemaDeGerenciamentoDeTarefas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

public class TaskRepositoryTests
{
    [Fact]
    public async Task GetAllTasks_ReturnsTasksList()
    {
       
        var mockRepository = new Mock<ITaskRepository>();
        mockRepository.Setup(repo => repo.GetAllTasks())
            .ReturnsAsync(new List<TasksToDo>
            {
                new TasksToDo { Id = 1, Description = "Task 1", IsCompleted = false },
                new TasksToDo { Id = 2, Description = "Task 2", IsCompleted = true }
            });

       
        var tasks = await mockRepository.Object.GetAllTasks();

        
        Assert.NotNull(tasks);
        Assert.Equal(2, tasks.Count());
    }

    [Fact]
    public async Task GetTaskById_ExistingId_ReturnsTask()
    {
       
        var mockRepository = new Mock<ITaskRepository>();
        var taskId = 1;
        mockRepository.Setup(repo => repo.GetTaskById(taskId))
            .ReturnsAsync(new TasksToDo { Id = taskId, Description = "Task 1", IsCompleted = false });

       
        var task = await mockRepository.Object.GetTaskById(taskId);

       
        Assert.NotNull(task);
        Assert.Equal(taskId, task.Id);
        Assert.Equal("Task 1", task.Description);
        Assert.False(task.IsCompleted);
    }

    [Fact]
    public async Task GetTaskById_NonExistingId_ReturnsNull()
    {
      
        var mockRepository = new Mock<ITaskRepository>();
        var nonExistingTaskId = 100;
        mockRepository.Setup(repo => repo.GetTaskById(nonExistingTaskId))
            .ReturnsAsync((TasksToDo)null);

       
        var task = await mockRepository.Object.GetTaskById(nonExistingTaskId);

      
        Assert.Null(task);
    }

    [Fact]
    public async Task CreateTask_ValidTask_ReturnsTaskId()
    {
        
        var mockRepository = new Mock<ITaskRepository>();
        var newTask = new TasksToDo { Description = "New Task", IsCompleted = false };
        var expectedTaskId = 1;
        mockRepository.Setup(repo => repo.CreateTask(newTask))
            .ReturnsAsync(expectedTaskId);

       
        var taskId = await mockRepository.Object.CreateTask(newTask);

        
        Assert.Equal(expectedTaskId, taskId);
    }

    [Fact]
    public async Task UpdateTask_ExistingTask_ReturnsTrue()
    {
        
        var mockRepository = new Mock<ITaskRepository>();
        var existingTask = new TasksToDo { Id = 1, Description = "Task 1", IsCompleted = false };
        mockRepository.Setup(repo => repo.UpdateTask(existingTask))
            .ReturnsAsync(true);

       
        var result = await mockRepository.Object.UpdateTask(existingTask);

        
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateTask_NonExistingTask_ReturnsFalse()
    {
       
        var mockRepository = new Mock<ITaskRepository>();
        var nonExistingTask = new TasksToDo { Id = 100, Description = "Non-Existing Task", IsCompleted = false };
        mockRepository.Setup(repo => repo.UpdateTask(nonExistingTask))
            .ReturnsAsync(false);

        
        var result = await mockRepository.Object.UpdateTask(nonExistingTask);

       
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteTask_ExistingId_ReturnsTrue()
    {
        
        var mockRepository = new Mock<ITaskRepository>();
        var existingTaskId = 1;
        mockRepository.Setup(repo => repo.DeleteTask(existingTaskId))
            .ReturnsAsync(true);

       
        var result = await mockRepository.Object.DeleteTask(existingTaskId);

        
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteTask_NonExistingId_ReturnsFalse()
    {
       
        var mockRepository = new Mock<ITaskRepository>();
        var nonExistingTaskId = 100;
        mockRepository.Setup(repo => repo.DeleteTask(nonExistingTaskId))
            .ReturnsAsync(false);

        
        var result = await mockRepository.Object.DeleteTask(nonExistingTaskId);

        
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateTaskStatus_ExistingTaskId_ReturnsTrue()
    {
        
        var mockRepository = new Mock<ITaskRepository>();
        var existingTaskId = 1;
        var newStatus = true;
        mockRepository.Setup(repo => repo.UpdateTaskStatus(existingTaskId, newStatus))
            .ReturnsAsync(true);

        
        var result = await mockRepository.Object.UpdateTaskStatus(existingTaskId, newStatus);

        
        Assert.True(result);
    }

    [Fact]
    
    public async Task UpdateTaskStatus_NonExistingTaskId_ReturnsFalse()
    {
        var mockRepository = new Mock<ITaskRepository>();
        var nonExistingTaskId = 100;
        var newStatus = true;
        mockRepository.Setup(repo => repo.UpdateTaskStatus(nonExistingTaskId, newStatus))
            .ReturnsAsync(false);

        
        var result = await mockRepository.Object.UpdateTaskStatus(nonExistingTaskId, newStatus);

        Assert.False(result);
    }
}
