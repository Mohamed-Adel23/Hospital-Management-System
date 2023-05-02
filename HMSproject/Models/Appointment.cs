using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;

namespace HMSproject.Models;

[Authorize]
public partial class Appointment
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Patient")]
    public string?  PatientID{ get; set; }
    
    [Required]
    [ForeignKey("Department")]
    [Display(Name = "Department")]
    public int DepartmentID { get; set; }
    
    [Required]
    public string Day { get; set; }
    [Required]
    public int? Status { get; set; }
    
    public virtual Department? Department { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual ICollection<Diagnose>? Diagnoses { get; set; } = new List<Diagnose>();

    public virtual ICollection<Lab>? Labs { get; set; } = new List<Lab>();
}
