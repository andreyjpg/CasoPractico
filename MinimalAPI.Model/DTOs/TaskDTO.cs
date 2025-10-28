using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalAPI.Model.DTOs
{
    public class TaskDTO
    {

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string Status { get; set; } = null!;

        public DateTime DueDate { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool? Approved { get; set; }
    }
}
