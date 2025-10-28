using Microsoft.EntityFrameworkCore;
using MinimalAPI.Model;
using TaskModel = MinimalAPI.Model.Task;

namespace MinimalAPI.Context
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        public DbSet<TaskModel> Task => Set<TaskModel>();
        public DbSet<User> User => Set<User>();
    }
}
