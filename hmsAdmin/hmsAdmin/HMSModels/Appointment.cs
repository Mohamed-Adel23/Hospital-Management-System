using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.HMSModels;

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

    [Column("fk_tt")]
    public int? FkTt { get; set; }

    [Column("fk_pay")]
    public int? FkPay { get; set; }

    [Column("fk_stat")]
    public int? FkStat { get; set; }

    [InverseProperty("FkAppNavigation")]
    public virtual ICollection<Diagnose> Diagnoses { get; set; } = new List<Diagnose>();

    [ForeignKey("FkDept")]
    [InverseProperty("Appointments")]
    public virtual Department? FkDeptNavigation { get; set; }

    [ForeignKey("FkPat")]
    [InverseProperty("Appointments")]
    public virtual Patient? FkPatNavigation { get; set; }

    [ForeignKey("FkPay")]
    [InverseProperty("Appointments")]
    public virtual Payment? FkPayNavigation { get; set; }

    [ForeignKey("FkStat")]
    [InverseProperty("Appointments")]
    public virtual Status? FkStatNavigation { get; set; }

    [ForeignKey("FkTt")]
    [InverseProperty("Appointments")]
    public virtual Timetable? FkTtNavigation { get; set; }

    [InverseProperty("FkAppNavigation")]
    public virtual ICollection<Lab> Labs { get; set; } = new List<Lab>();
}
