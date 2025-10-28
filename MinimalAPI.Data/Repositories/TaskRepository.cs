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
    }

    public class RepositoryTask : RepositoryBase<TaskModel>, IRepositoryTask
    { }

}
