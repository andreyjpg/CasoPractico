using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Core.BusinessLogic;
using MinimalAPI.Data.Models;
using System.Threading.Tasks;

namespace MinimalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserBusiness userBusiness) : ControllerBase
    {

        [HttpGet]
        public async Task<IEnumerable<User>> GetAsync()
        {
            return await userBusiness.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<User> GetByIdAsync(int id)
        {
            return await userBusiness.GetByIdAsync(id);
        }

    }
}
