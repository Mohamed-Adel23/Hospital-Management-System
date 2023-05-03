using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.Models;

[Table("medicine")]
public partial class Medicine
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("fk_dia")]
    public int? FkDia { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [ForeignKey("FkDia")]
    [InverseProperty("Medicines")]
    public virtual Diagnose? FkDiaNavigation { get; set; }
}
