using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskModel = MinimalAPI.Data.Models.Task;
using MinimalAPI.Data.Repositories;

namespace MinimalAPI.Core.BusinessLogic
{

    public interface ITaskBusiness
    {

        Task<IEnumerable<TaskModel>> GetAsync();
        Task<TaskModel> GetByIdAsync(int id);

        Task<bool> SaveTaskAsync(TaskModel Task);

        Task<bool> CreateAsync(TaskModel task);
    }

    public class TaskBusiness (IRepositoryTask repository) : ITaskBusiness
    {
        public Task<IEnumerable<TaskModel>> GetAsync()
        {
            return repository.ReadAsync();
        }

        public Task<TaskModel> GetByIdAsync(int id)
        {
            return repository.FindAsync(id);
        }

        public Task<bool> CreateAsync(TaskModel task)
        {
            return repository.CreateAsync(task);
        }

        public Task<bool> SaveTaskAsync(TaskModel entity)
        {
            return repository.UpdateAsync(entity);
        }
    }

    
}
