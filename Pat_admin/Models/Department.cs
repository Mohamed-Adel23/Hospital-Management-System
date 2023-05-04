using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSproject.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int app_price { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("FkDeptNavigation")]
    public virtual ICollection<Doctor>? Doctors { get; set; } = new List<Doctor>();

    [InverseProperty("FkDeptNavigation")]
    public virtual ICollection<Nurse>? Nurses { get; set; } = new List<Nurse>();
}