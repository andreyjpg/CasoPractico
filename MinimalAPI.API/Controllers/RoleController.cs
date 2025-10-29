using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Core.BusinessLogic;
using MinimalAPI.Data.Models;
using System.Text.Json;
using System.Threading.Tasks;

namespace MinimalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IRoleBusiness RoleBusiness) : ControllerBase
    {

        [HttpGet]
        public async Task<IEnumerable<Role>> GetAsync()
        {
            return await RoleBusiness.GetAsync();
        }
        

        [HttpPost]
        public async Task<bool> CreateAsync([FromBody] string entity)
        {
            var model = JsonSerializer.Deserialize<Role>(entity);

            if (model == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return await RoleBusiness.CreateAsync(model);
        }
    }
}
