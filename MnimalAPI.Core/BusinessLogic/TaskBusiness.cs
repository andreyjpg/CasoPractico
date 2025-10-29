using System;
using System.Collections.Generic;
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
        Task<bool> ApproveTaskAsync(int id);
        Task<bool> DenyTaskAsync(int id);
    }

    public class TaskBusiness : ITaskBusiness
    {
        private readonly IRepositoryTask _repository;

        public TaskBusiness(IRepositoryTask repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TaskModel>> GetAsync()
        {
            return _repository.ReadAsync();
        }

        public Task<TaskModel> GetByIdAsync(int id)
        {
            return _repository.FindAsync(id);
        }

        public Task<bool> CreateAsync(TaskModel task)
        {
            return _repository.CreateAsync(task);
        }

        public Task<bool> SaveTaskAsync(TaskModel entity)
        {
            return _repository.UpdateAsync(entity);
        }

        // Bloque de aprobaciones y denegaciones por restricciones de tiempo
        public async Task<bool> ApproveTaskAsync(int id)
        {
            var task = await _repository.FindAsync(id);
            if (task == null) return false;

            if (task.Approved == false)
            {
                if (task.CreatedAt.HasValue)
                {
                    var hoursSinceCreation = (DateTime.UtcNow - task.CreatedAt.Value.ToUniversalTime()).TotalHours;

                    if (hoursSinceCreation > 24)
                        return false;
                }
                else
                {
                    return false;
                }
            }

            return await _repository.ApproveAsync(id);
        }

        public async Task<bool> DenyTaskAsync(int id)
        {
            var task = await _repository.FindAsync(id);
            if (task == null) return false;

            return await _repository.DenyAsync(id);
        }
    }
}
