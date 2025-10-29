using MinimalAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskModel = MinimalAPI.Data.Models.Task;


namespace MinimalAPI.Data.Repositories
{
    public interface IRepositoryTask
    {
        Task<bool> CreateAsync(TaskModel entity);
        Task<IEnumerable<TaskModel>> ReadAsync();
        Task<TaskModel> FindAsync(int id);
        Task<bool> UpdateAsync(TaskModel entity);
        Task<bool> DeleteAsync(TaskModel id);
        Task<bool> ApproveAsync(int id);
        Task<bool> DenyAsync(int id);
    }

    public class RepositoryTask : RepositoryBase<TaskModel>, IRepositoryTask
    {
        // Aprueba una tarea (Approved = true)
        public async Task<bool> ApproveAsync(int id)
        {
            using var context = new TaskDbContext();
            var task = await context.Tasks.FindAsync(id);
            if (task == null) return false;

            task.Approved = true;
            task.Status = "Approved";
            await context.SaveChangesAsync();
            return true;
        }

        // Deniega una tarea (Approved = false)
        public async Task<bool> DenyAsync(int id)
        {
            using var context = new TaskDbContext();
            var task = await context.Tasks.FindAsync(id);
            if (task == null) return false;

            task.Approved = false;
            task.Status = "Denied";
            await context.SaveChangesAsync();
            return true;
        }
    }
}
