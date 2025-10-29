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
        Task<bool> AssignRole(UserRole entity);

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

        public async Task<bool> AssignRole(UserRole entity)
        {
            try
            {
                var existing = await _context.UserRoles
                    .FirstOrDefaultAsync(ur => ur.UserId == entity.UserId);

                if (existing != null)
                {
                    _context.UserRoles.Remove(existing);
                    await _context.SaveChangesAsync();
                }

                var newUserRole = new UserRole { UserId = entity.UserId, RoleId = entity.RoleId };
                await _context.UserRoles.AddAsync(newUserRole);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
            

        }

    }
}
