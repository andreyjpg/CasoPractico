using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Core.BusinessLogic;
using TaskModel = MinimalAPI.Data.Models.Task;
using System.Threading.Tasks;
using MinimalAPI.Data.Repositories;

namespace MinimalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(ITaskBusiness taskBusiness) : ControllerBase
    {

        [HttpGet]
        public async Task<IEnumerable<TaskModel>> GetAsync()
        {
            return await taskBusiness.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<TaskModel> GetById(int id)
        {
            return await taskBusiness.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<bool> CreateAsync(TaskModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return await taskBusiness.CreateAsync(entity);
        }

        public Task<bool> SaveTaskAsync(TaskModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return taskBusiness.SaveTaskAsync(entity);
        }
    }
}
