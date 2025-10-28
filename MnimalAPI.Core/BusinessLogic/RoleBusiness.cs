using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalAPI.Data.Models;
using MinimalAPI.Data.Repositories;

namespace MinimalAPI.Core.BusinessLogic
{
    public interface IRoleBusiness
    {
        Task<IEnumerable<Role>> GetAsync();
        Task<bool> CreateAsync(Role Entity);
    }

    public class RoleBusiness(IRepositoryRole repository) : IRoleBusiness
    {
        public async Task<IEnumerable<Role>> GetAsync()
        {
            return await repository.ReadAsync();
        }
        public async Task<bool> CreateAsync(Role entity)
        {
            return await repository.CreateAsync(entity);
        }
    }
}
