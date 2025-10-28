using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Core.BusinessLogic;
using MinimalAPI.Data.Models;
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
        public async Task<bool> CreateAsync(Role entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return await RoleBusiness.CreateAsync(entity);
        }
    }
}
