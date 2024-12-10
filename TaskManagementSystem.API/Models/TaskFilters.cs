namespace TaskManagementSystem.API.Models
{
    public class TaskFilters
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        private List<string> _status = [];
        public List<string> Status
        {
            get => _status;
            set
            {
                _status = value.SelectMany(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        private DateTime dueDate = DateTime.MinValue;
        public DateTime DueDate
        {
            get => dueDate;
            set => dueDate = value;
        }
    }
}