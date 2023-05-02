using System;
using System.Collections.Generic;

namespace HMSproject.Models;

public partial class Diagnose
{
    public int Id { get; set; }

    public int? FkApp { get; set; }

    public string? Description { get; set; }

    public string Prescription { get; set; } = null!;

    public string? Analysis { get; set; }

    public DateTime? Date { get; set; }

    public virtual Appointment? FkAppNavigation { get; set; }
}
