using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Core.BusinessLogic;
using TaskModel = MinimalAPI.Data.Models.Task;
using System.Threading.Tasks;
using MinimalAPI.Data.Repositories;
using System.Text.Json;

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
        public async Task<bool> CreateAsync([FromBody] string entity)
        {
            var model = JsonSerializer.Deserialize<TaskModel>(entity);
            if (model == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return await taskBusiness.CreateAsync(model);
        }

        [HttpPut]
        public Task<bool> SaveTaskAsync([FromBody] TaskModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return taskBusiness.SaveTaskAsync(entity);
        }

        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> ApproveTask(int id)
        {
            var result = await taskBusiness.ApproveTaskAsync(id);

            if (!result)
                return BadRequest("No se puede aprobar esta tarea (ya fue denegada hace más de 24h o no existe).");

            return Ok("Tarea aprobada correctamente.");
        }

        [HttpPut("Deny/{id}")]
        public async Task<IActionResult> DenyTask(int id)
        {
            var result = await taskBusiness.DenyTaskAsync(id);

            if (!result)
                return BadRequest("No se pudo denegar la tarea (no existe o error en la base de datos).");

            return Ok("Tarea denegada correctamente.");
        }
    }
}
