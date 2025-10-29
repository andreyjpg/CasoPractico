using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Core.BusinessLogic;
using MinimalAPI.Data.Models;
using System.Text.Json;
using System.Threading.Tasks;

namespace MinimalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController(IUserRoleBusiness userRoleBusiness) : ControllerBase
    {

        [HttpPost]
        public async Task<bool> AssignRole([FromBody] string entity)
        {
            var model = JsonSerializer.Deserialize<UserRole>(entity);
            if(model == null ) {
                throw new ArgumentNullException(nameof(entity));
            }
            return await userRoleBusiness.AssignRole(model);
        }

    }
}
