using MinimalAPI.Data.Models;

namespace MinimalAPI.MVC.Models
{
    public class RolesViewModel
    {
        public IEnumerable<User> Users { get; set; } = new List<User>();
        public IEnumerable<Role> Roles { get; set; } = new List<Role>();
        public string? NewRoleName { get; set; }

    }

}
