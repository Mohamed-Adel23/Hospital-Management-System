using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSproject.Models;

public partial class Diagnose
{
    public int Id { get; set; }
    
    [ForeignKey("Appointments")]
    public int? fk_app { get; set; }

    public string? Description { get; set; }

    public string Prescription { get; set; } = null!;

    public string? Analysis { get; set; }

    public DateTime? Date { get; set; } = DateTime.Now;

    [InverseProperty("Diagnoses")]
    public virtual Appointment? Appointments { get; set; }
}
