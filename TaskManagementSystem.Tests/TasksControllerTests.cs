using System.Net;
using TaskManagementSystem.API.Controllers;
using TaskManagementSystem.API.Models;
using TaskManagementSystem.API.Repository;
using TaskManagementSystem.API.Services;

namespace TaskManagementSystem.Tests
{
    public class TasksControllerTests
    {
        private readonly IRepository repository;
        private readonly TaskService _taskService;
        private TasksController tasksController;

        public TasksControllerTests()
        {
            repository = new Repository();
             _taskService = new(repository);
             tasksController = new(_taskService);
        }
        

        [Fact]
        public void GetAll_ReturnsOkResult()
        {
            var result = tasksController.GetTasks(new TaskFilters());
            ApiBaseResponse<List<API.Models.Task>> response = result.Value!;
            Assert.True(response.Message == "Tasks found");
            Assert.True(response.StatusCode == (int)HttpStatusCode.OK);
            Assert.True(response.Content.Count == 6);
        }

        [Fact]
        public void GetTask_ReturnsOkResult()
        {
            var result = tasksController.GetTask(1);
            ApiBaseResponse<API.Models.Task> response = result.Value!;
            Assert.True(response.Message == "Task found");
            Assert.True(response.StatusCode == (int)HttpStatusCode.OK);
            Assert.True(response.Content.Id == 1);
        }

        [Fact]
        public void GetTask_ReturnsNotFoundResult()
        {
            var result = tasksController.GetTask(111);
            ApiBaseResponse<API.Models.Task> response = result.Value!;
            Assert.True(response.Message == "Task not found");
            Assert.True(response.StatusCode == (int)HttpStatusCode.NotFound);
            Assert.True(response.Content == null);
        }

        [Fact]
        public void CreateTask_ReturnsOkResult()
        {
            var task = new API.Models.Task
            {
                Title = "Task",
                Description = "Task Description",
                DueDate = DateTime.Now.AddDays(7),
                Status = API.Models.TaskStatus.InProgress.ToString()
            };

            var result = tasksController.CreateTask(task);
            ApiBaseResponse<bool> response = result.Value!;
            Assert.True(response.Message == "Task created");
            Assert.True(response.StatusCode == (int)HttpStatusCode.Created);
            Assert.True(response.Content);
        }

        [Fact]
        public void UpdateTask_ReturnsOkResult()
        {
            var task = new API.Models.Task
            {
                Title = "Task",
                Description = "Task Description",
                DueDate = DateTime.Now.AddDays(7),
                Status = API.Models.TaskStatus.InProgress.ToString()
            };

            var result = tasksController.UpdateTask(1, task);
            ApiBaseResponse<bool> response = result.Value!;
            Assert.True(response.Message == "Task updated");
            Assert.True(response.StatusCode == (int)HttpStatusCode.OK);
            Assert.True(response.Content);
        }

    }
}