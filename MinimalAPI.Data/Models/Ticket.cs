using System;
using System.Collections.Generic;

namespace MinimalAPI.Data.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? Description { get; set; }

    public bool Status { get; set; }
}
