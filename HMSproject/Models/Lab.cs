using System;
using System.Collections.Generic;

namespace HMSproject.Models;

public partial class Lab
{
    public int Id { get; set; }

    public int? FkApp { get; set; }

    public string AnaName { get; set; } = null!;

    public string Result { get; set; } = null!;

    public int Cost { get; set; }

    public DateTime? Date { get; set; }

    public virtual Appointment? FkAppNavigation { get; set; }
}
