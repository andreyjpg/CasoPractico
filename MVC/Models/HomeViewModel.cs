using TaskModel = MinimalAPI.Data.Models.Task;

namespace MVC.Models
{
    public class HomeViewModel
    {
        public string Title { get; set; } = "Tickets";
        public IEnumerable<TaskModel> Tickets { get; set; } = [];
        public TaskModel? NewTask { get; set; } 
    }
}
