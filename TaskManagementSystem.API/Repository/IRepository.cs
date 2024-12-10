using TaskManagementSystem.API.Models;

namespace TaskManagementSystem.API.Repository
{
    public interface IRepository
    {
        List<Models.Task> GetTasks(TaskFilters taskFilters);
        Models.Task? GetTask(int id);
        void CreateTask(Models.Task task);
        void UpdateTask(int id, Models.Task task);
        void DeleteTask(int id);
    }
}