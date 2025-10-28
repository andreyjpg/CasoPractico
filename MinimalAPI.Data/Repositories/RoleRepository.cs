using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalAPI.Data.Models;

namespace MinimalAPI.Data.Repositories
{
    public interface IRepositoryRole
    {
        Task<bool> CreateAsync(Role entity);
        Task<IEnumerable<Role>> ReadAsync();
    }

    public class RepositoryRole : RepositoryBase<Role>, IRepositoryRole
    {
    }
}
