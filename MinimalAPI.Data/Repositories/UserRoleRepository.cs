using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalAPI.Data.Repositories
{
    public interface IRepositoryUserRole
    {
        Task<bool> CreateAsync(UserRole entity);
        Task<bool> UpdateAsync(UserRole entity);
        Task<IEnumerable<UserRole>> ReadAsync();
        Task<UserRole> FindAsyncByUserId(int userId);
    }

    public class RepositoryUserRole : RepositoryBase<UserRole>, IRepositoryUserRole
    {
        private readonly TaskDbContext _context;
        protected new TaskDbContext DbContext => _context;
        protected new DbSet<UserRole> DbSet;

        public RepositoryUserRole()
        {
            _context = new TaskDbContext();
            DbSet<UserRole> _sdbSet = _context.Set<UserRole>();
        }

        public async Task<UserRole> FindAsyncByUserId(int userId)
        {
            try
            {
                return await _context.UserRoles.FirstOrDefaultAsync(r => r.UserId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
