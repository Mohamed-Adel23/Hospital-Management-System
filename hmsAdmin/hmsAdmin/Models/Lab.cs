using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.Models;

[Table("lab")]
public partial class Lab
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("fk_app")]
    public int? FkApp { get; set; }

    [Column("ana_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string AnaName { get; set; } = null!;

    [Column("result")]
    [StringLength(20)]
    [Unicode(false)]
    public string Result { get; set; } = null!;

    [Column("cost")]
    public int Cost { get; set; }

    [Column("date", TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [ForeignKey("FkApp")]
    [InverseProperty("Labs")]
    public virtual Appointment? FkAppNavigation { get; set; }
}
