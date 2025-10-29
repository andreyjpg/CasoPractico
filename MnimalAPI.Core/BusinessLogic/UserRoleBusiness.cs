using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalAPI.Data.Models;

using MinimalAPI.Data.Repositories;

namespace MinimalAPI.Core.BusinessLogic
{
    public interface IUserRoleBusiness
    {
        Task<bool> AssignRole(UserRole entity);

    }

    public class UserRoleBusiness(IRepositoryUserRole repository) : IUserRoleBusiness
    {
        public Task<bool> AssignRole(UserRole entity)
        {
            return repository.AssignRole(entity);
        }
        
    }
}
