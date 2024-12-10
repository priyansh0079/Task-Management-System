using TaskManagementSystem.API.Models;

namespace TaskManagementSystem.API.Repository
{
    public class Repository : IRepository
    {
        private static readonly List<Models.Task> tasks = new()
        {
            new Models.Task
            {
                Id = 1,
                Title = "Prepare project proposal",
                Description = "Draft the initial version of the project proposal for the new client.",
                Status = "Pending",
                DueDate = DateTime.Parse("2024-12-15T12:00:00Z")
            },
            new Models.Task
            {
                Id = 2,
                Title = "Team meeting for sprint planning",
                Description = "Organize a meeting to discuss tasks and objectives for the upcoming sprint.",
                Status = "InProgress",
                DueDate = DateTime.Parse("2024-12-12T09:30:00Z")
            },
            new Models.Task
            {
                Id = 3,
                Title = "Code review for module A",
                Description = "Review the codebase of module A and provide feedback.",
                Status = "Pending",
                DueDate = DateTime.Parse("2024-12-13T17:00:00Z")
            },
            new Models.Task
            {
                Id = 4,
                Title = "Finalize UI design",
                Description = "Complete the user interface design for the dashboard.",
                Status = "Completed",
                DueDate = DateTime.Parse("2024-12-05T18:00:00Z")
            },
            new Models.Task
            {
                Id = 5,
                Title = "Prepare test cases",
                Description = "Create test cases for the new API endpoints.",
                Status = "Pending",
                DueDate = DateTime.Parse("2024-12-18T10:00:00Z")
            },
            new Models.Task
            {
                Id = 6,
                Title = "Database schema update",
                Description = "Update the database schema to include new fields required by the application.",
                Status = "InProgress",
                DueDate = DateTime.Parse("2024-12-10T14:00:00Z")
            },
            new Models.Task
            {
                Id = 7,
                Title = "Client presentation",
                Description = "Prepare slides and deliver the presentation to the client.",
                Status = "Pending",
                DueDate = DateTime.Parse("2024-12-20T11:00:00Z")
            },
            new Models.Task
            {
                Id = 8,
                Title = "Bug fixing in login module",
                Description = "Fix the reported bugs in the login functionality of the application.",
                Status = "InProgress",
                DueDate = DateTime.Parse("2024-12-14T16:00:00Z")
            },
            new Models.Task
            {
                Id = 9,
                Title = "Write technical documentation",
                Description = "Document the architecture and API specifications for the system.",
                Status = "Pending",
                DueDate = DateTime.Parse("2024-12-22T17:00:00Z")
            },
            new Models.Task
            {
                Id = 10,
                Title = "Update user permissions",
                Description = "Modify the user permissions as per the new roles defined by the management.",
                Status = "Completed",
                DueDate = DateTime.Parse("2024-12-07T15:00:00Z")
            }
        };

        public void CreateTask(Models.Task task)
        {
            task.Id = tasks.Count + 1;
            tasks.Add(task);
        }

        public void DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return;
            tasks.Remove(task);
        }

        public Models.Task? GetTask(int id)
        {
            Models.Task? task = tasks.FirstOrDefault(t => t.Id == id);
            return task;
        }

        public List<Models.Task> GetTasks(TaskFilters taskFilters)
        {
            List<Models.Task> filteredTasks = tasks;
            if (taskFilters.Status.Count > 0)
            {
                filteredTasks = filteredTasks.Where(t => taskFilters.Status.Contains(t.Status)).ToList();
            }

            if (taskFilters.DueDate != DateTime.MinValue)
            {
                filteredTasks = filteredTasks.Where(t => t.DueDate.Date <= taskFilters.DueDate.Date).ToList();
            }

            return filteredTasks.Skip((taskFilters.PageNumber - 1) * taskFilters.PageSize).Take(taskFilters.PageSize).ToList();
        }

        public void UpdateTask(int id, Models.Task task)
        {
            Models.Task? existingTask = tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask == null) return;

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.DueDate = task.DueDate;
        }
    }
}