using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.HMSModels;

[Keyless]
[Table("medicine")]
public partial class Medicine
{
    [Column("fk_dia")]
    public int? FkDia { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [ForeignKey("FkDia")]
    public virtual Diagnose? FkDiaNavigation { get; set; }
}
