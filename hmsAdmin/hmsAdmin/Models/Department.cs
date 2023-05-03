using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.Models;

[Table("departments")]
public partial class Department
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Dept name must be between 3 and 50 characters")]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("app_price")]
    [DisplayName("Appointment Cost")]
    [Range(10, 1000, ErrorMessage = "Cost must be between 10 and 1000")]
    public int AppPrice { get; set; }

 //   [Required]
 //   [Column("dr_number")]
 //   [DisplayName("Dr Number")]
 //   [Range(1, 10, ErrorMessage = "Dr Number must be between 1 and 10")]
 //   public int? DrNumber { get; set; }

 //   [Required]
 //   [Column("ns_number")]
	//[DisplayName("Nurse Number")]
	//[Range(2, 20, ErrorMessage = "Dr Number must be between 2 and 20")]
 //   public int? NsNumber { get; set; }

    [InverseProperty("FkDeptNavigation")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("FkDeptNavigation")]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    [InverseProperty("FkDeptNavigation")]
    public virtual ICollection<Nurse> Nurses { get; set; } = new List<Nurse>();
}
