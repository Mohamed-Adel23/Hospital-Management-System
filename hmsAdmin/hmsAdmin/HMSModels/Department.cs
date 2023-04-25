using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.HMSModels;

[Table("departments")]
public partial class Department
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("app_price")]
    public int AppPrice { get; set; }

    [Column("dr_number")]
    public int? DrNumber { get; set; }

    [Column("ns_number")]
    public int? NsNumber { get; set; }

    [InverseProperty("FkDeptNavigation")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("FkDeptNavigation")]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    [InverseProperty("FkDeptNavigation")]
    public virtual ICollection<Nurse> Nurses { get; set; } = new List<Nurse>();
}
