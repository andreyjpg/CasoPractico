using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalAPI.Model.DTOs;

namespace MinimalAPI.Model.DTOs
{
    public class RoleDTO
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<UserRoleDTO> UserRoles { get; set; } = new List<UserRoleDTO>();
    }
}
