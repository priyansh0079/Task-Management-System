namespace TaskManagementSystem.API.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = TaskStatus.Pending.ToString();
        public DateTime DueDate { get; set; }
    }
}