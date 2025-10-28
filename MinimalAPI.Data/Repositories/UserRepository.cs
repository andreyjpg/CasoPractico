using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text;

namespace MinimalAPI.Data.Repositories
{
    public interface IRepositoryUser
    {
        Task<IEnumerable<User>> ReadAsync();
        Task<User> FindAsync(int id);
        Task<bool> UpdateAsync(User entity);
        Task<User> GetUserByCredentialsAsync(string email, string password);
    }

    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        private readonly TaskDbContext _context;
        protected new TaskDbContext DbContext => _context;
        protected new DbSet<User> DbSet;

        public RepositoryUser()
        {
            _context = new TaskDbContext();
            DbSet<User> _sdbSet = _context.Set<User>();
        }
        public async Task<User> GetUserByCredentialsAsync(string email, string password)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var userValidated = await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync((user) => 
                            user.Email == email && 
                            user.Password.SequenceEqual(hashBytes));
            if(userValidated == null)
            {
                throw new Exception("Not valid user");
            }
            return userValidated;
        }
        public new async Task<IEnumerable<User>> ReadAsync()
        {
            return await _context.Users.Include(u => u.UserRoles).ToListAsync();

        }

        public new async Task<User> FindAsync(int id)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.UserRoles)
                    .FirstOrDefaultAsync(u => u.UserId == id);

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
