using System.Globalization;
using System.Net;
using CsvHelper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.Models;
using TaskManagementSystem.API.Repository;
using TaskManagementSystem.API.Validations;

namespace TaskManagementSystem.API.Services
{
    public class TaskService
    {
        private readonly IRepository _repository;
        public TaskService(IRepository repository)
        {
            _repository = repository;
        }

        public ApiBaseResponse<List<Models.Task>> GetTasks(TaskFilters taskFilters)
        {
            ApiBaseResponse<List<Models.Task>> response = new();
            try
            {
                var tasks = _repository.GetTasks(taskFilters);
                if (tasks == null || tasks.Count == 0)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "No tasks found";
                    return response;
                }

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Tasks found";
                response.Content = tasks;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiBaseResponse<Models.Task> GetTask(int id)
        {
            ApiBaseResponse<Models.Task> response = new();
            try
            {
                Models.Task? task = _repository.GetTask(id);
                if (task == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Task not found";
                    return response;
                }

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Task found";
                response.Content = task;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }
    
        public ApiBaseResponse<bool> CreateTask(Models.Task task)
        {
            ApiBaseResponse<bool> response = new();
            try
            {
                CreateTaskValidation rules = new();
                ValidationResult validationResult = rules.Validate(task);

                if (!validationResult.IsValid)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage!;
                    response.Content = false;
                    return response;
                }

                _repository.CreateTask(task);

                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Task created";
                response.Content = true;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }
    
        public ApiBaseResponse<bool> UpdateTask(int id, Models.Task task)
        {
            ApiBaseResponse<bool> response = new();
            try
            {
                UpdateTaskValidation rules = new();
                ValidationResult validationResult = rules.Validate(task);

                if (!validationResult.IsValid)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage!;
                    response.Content = false;
                    return response;
                }

                _repository.UpdateTask(id, task);

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Task updated";
                response.Content = true;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }
    
        public ApiBaseResponse<bool> DeleteTask(int id)
        {
            ApiBaseResponse<bool> response = new();
            try
            {
                _repository.DeleteTask(id);

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Task deleted";
                response.Content = true;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}