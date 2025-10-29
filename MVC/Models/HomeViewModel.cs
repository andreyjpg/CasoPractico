using TaskModel = MinimalAPI.Data.Models.Task;

namespace MVC.Models
{
    public class HomeViewModel
    {
        public string Title { get; set; } = "Tickets";
        public IEnumerable<TaskModel> Tickets { get; set; } = [];
        public TaskModel? NewTask { get; set; } 

        public ApiResponse? ApiResponse { get; set; }  
    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
