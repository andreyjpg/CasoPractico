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
        Task<bool> CreateAsync(UserRole entity);
        Task<bool> UpdateAsync(UserRole entity);

        Task<IEnumerable<UserRole>> ReadAsync();
        Task<UserRole> FindAsyncByUserId(int userId);

    }

    public class UserRoleBusiness(IRepositoryUserRole repository) : IUserRoleBusiness
    {
        public Task<bool> CreateAsync(UserRole entity)
        {
            return repository.CreateAsync(entity);
        }
        public Task<bool> UpdateAsync(UserRole entity)
        {
            return repository.UpdateAsync(entity);
        }
        public Task<IEnumerable<UserRole>> ReadAsync()
        {
            return repository.ReadAsync();
        }
        public Task<UserRole> FindAsyncByUserId(int userId)
        {
            return repository.FindAsyncByUserId(userId);
        }
    }
}
