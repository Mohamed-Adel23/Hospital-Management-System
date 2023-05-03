using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.Models;

[Table("diagnose")]
public partial class Diagnose
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("fk_app")]
    public int? FkApp { get; set; }

    [Column("description")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("prescription")]
    [StringLength(255)]
    [Unicode(false)]
    public string Prescription { get; set; } = null!;

    [Column("analysis")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Analysis { get; set; }

    [Column("date", TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [ForeignKey("FkApp")]
    [InverseProperty("Diagnoses")]
    public virtual Appointment? FkAppNavigation { get; set; }

    [InverseProperty("FkDiaNavigation")]
    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
