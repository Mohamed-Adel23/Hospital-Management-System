using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.Models;

[Table("appointments")]
public partial class Appointment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("price")]
    public int Price { get; set; }

    [Column("fk_pat")]
    public int? FkPat { get; set; }

    [Column("fk_dept")]
    public int? FkDept { get; set; }

    [Column("status")]
    public int Status { get; set; }

	[Column("day")]
	[StringLength(50)]
	[Unicode(false)]
	public string day { get; set; } = null!;

	[InverseProperty("FkAppNavigation")]
    public virtual ICollection<Diagnose> Diagnoses { get; set; } = new List<Diagnose>();

    [ForeignKey("FkDept")]
    [InverseProperty("Appointments")]
    public virtual Department? FkDeptNavigation { get; set; }

    [ForeignKey("FkPat")]
    [InverseProperty("Appointments")]
    public virtual Patient? FkPatNavigation { get; set; }

    [InverseProperty("FkAppNavigation")]
    public virtual ICollection<Lab> Labs { get; set; } = new List<Lab>();
}
