using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalAPI.Data.Models;
using MinimalAPI.Data.Repositories;

namespace MinimalAPI.Core.BusinessLogic
{

    public interface IUserBusiness
    {

        Task<IEnumerable<User>> GetAsync();

        Task<bool> UpdateAsync(User User);

        Task<User> GetByIdAsync(int Id);

        Task<User> GetByCredentialsAsync(string email,  string password);
            

    }

    public class UserBusiness(IRepositoryUser repository) : IUserBusiness
    {
        public Task<IEnumerable<User>> GetAsync()
        {
            return repository.ReadAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            return repository.FindAsync(id);
        }

        public Task<bool> UpdateAsync(User entity)
        {
            return repository.UpdateAsync(entity);
        }

        public Task<User> GetByCredentialsAsync(string email, string password)
        {
            return repository.GetUserByCredentialsAsync(email, password);
        }
        
    }
   
}
