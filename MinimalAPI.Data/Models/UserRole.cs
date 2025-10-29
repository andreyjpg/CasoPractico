using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MinimalAPI.Data.Models;

public partial class UserRole
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string? Description { get; set; }

    [JsonIgnore]
    public virtual Role Role { get; set; } = null!;

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
