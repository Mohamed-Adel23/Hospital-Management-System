using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.HMSModels;

[Table("timetable")]
public partial class Timetable
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("day_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string DayName { get; set; } = null!;

    [Column("day_date", TypeName = "date")]
    public DateTime? DayDate { get; set; }

    [InverseProperty("FkTtNavigation")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
