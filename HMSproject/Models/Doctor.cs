using System;
using System.Collections.Generic;

namespace HMSproject.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int? FkDept { get; set; }

    public int Age { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Experience { get; set; }

    public string PersonalProfile { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Image { get; set; }

    public virtual Department? FkDeptNavigation { get; set; }
}
