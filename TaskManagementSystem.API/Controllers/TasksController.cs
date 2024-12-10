using System.Globalization;
using System.Net;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.Models;
using TaskManagementSystem.API.Services;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController: ControllerBase
    {
        private readonly TaskService _taskService;
        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Get a list of all tasks
        /// </summary>
        /// <param name="taskFilters"></param>
        /// <returns>Returns a list of tasks</returns>
        [HttpGet]
        [Route("list")]
        public ActionResult<ApiBaseResponse<List<Models.Task>>> GetTasks([FromQuery] TaskFilters taskFilters)
        {
            ApiBaseResponse<List<Models.Task>> tasks = _taskService.GetTasks(taskFilters);
            return tasks;
        }

        /// <summary>
        /// Get a task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the task with the specified id</returns>
        [HttpGet]
        [Route("get/{id}")]
        public ActionResult<ApiBaseResponse<Models.Task>> GetTask(int id)
        {
            ApiBaseResponse<Models.Task> task = _taskService.GetTask(id);
            return task;
        }

        /// <summary>
        /// Create a new task
        /// </summary>
        /// <param name="task"></param>
        /// <returns>Returns true if the task was created successfully</returns>
        [HttpPost]
        [Route("create")]
        public ActionResult<ApiBaseResponse<bool>> CreateTask(Models.Task task)
        {
            ApiBaseResponse<bool> response = _taskService.CreateTask(task);
            return response;
        }

        /// <summary>
        /// Update an existing task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="task"></param>
        /// <returns>Returns true if the task was updated successfully</returns>
        [HttpPut]
        [Route("update/{id}")]
        public ActionResult<ApiBaseResponse<bool>> UpdateTask(int id, Models.Task task)
        {
            ApiBaseResponse<bool> response = _taskService.UpdateTask(id, task);
            return response; 
        }

        /// <summary>
        /// Delete a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if the task was deleted successfully</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public ActionResult<ApiBaseResponse<bool>> DeleteTask(int id)
        {
            ApiBaseResponse<bool> response = _taskService.DeleteTask(id);
            return response;
        }

        /// <summary>
        /// Export tasks to CSV
        /// </summary>
        /// <returns>Returns a CSV file containing all tasks</returns>
        [HttpGet("export")]
        public ActionResult ExportTasksToCsv()
        {
            try
            {
                var tasks = _taskService.GetTasks(new TaskFilters()).Content;
                var memoryStream = new MemoryStream();

                using (var writer = new StreamWriter(memoryStream, leaveOpen: true))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    // Write records to CSV
                    csv.WriteRecords(tasks);
                }

                memoryStream.Position = 0;

                var fileName = $"tasks-{DateTime.UtcNow:yyyyMMddHHmmss}.csv";

                return File(memoryStream, "text/csv", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiBaseResponse<bool>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Content = false
                });
            }
        }
    }
}