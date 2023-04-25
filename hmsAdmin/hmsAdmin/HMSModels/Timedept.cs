using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.HMSModels;

[Keyless]
[Table("timedept")]
public partial class Timedept
{
    [Column("fk_dept")]
    public int? FkDept { get; set; }

    [Column("fk_time")]
    public int? FkTime { get; set; }

    [Column("available_dr")]
    public int AvailableDr { get; set; }

    [Column("available_app")]
    public int AvailableApp { get; set; }

    [ForeignKey("FkDept")]
    public virtual Department? FkDeptNavigation { get; set; }

    [ForeignKey("FkTime")]
    public virtual Timetable? FkTimeNavigation { get; set; }
}
