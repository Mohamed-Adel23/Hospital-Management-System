using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMSproject.Models;

public partial class Department
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;

    public int app_price { get; set; }

    public virtual ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Doctor>? Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Nurse>? Nurses { get; set; } = new List<Nurse>();
}
