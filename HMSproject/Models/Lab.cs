using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSproject.Models;

public partial class Lab
{
    public int Id { get; set; }

    [ForeignKey("Appointments")]
    public int? FkApp { get; set; }

    public string AnaName { get; set; } = null!;

    public string Result { get; set; } = null!;

    public int Cost { get; set; }

    public DateTime? Date { get; set; }
    
    [InverseProperty("Labs")]
    public virtual Appointment? Appointments { get; set; }
}
