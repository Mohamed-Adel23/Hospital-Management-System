using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMSproject.Models;

public partial class Department
{
    public int Id { get; set; }

    [StringLength(50, MinimumLength = 3, ErrorMessage = "Dept name must be between 3 and 50 characters")]
    public string Name { get; set; } = null!;

    [DisplayName("Appointment Cost")]
    [Range(10, 1000, ErrorMessage = "Cost must be between 10 and 1000")]
    public int app_price { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 200 characters")]
    public string? Description { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Image { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("FkDeptNavigation")]
    public virtual ICollection<Doctor>? Doctors { get; set; } = new List<Doctor>();

    [InverseProperty("FkDeptNavigation")]
    public virtual ICollection<Nurse>? Nurses { get; set; } = new List<Nurse>();
}