using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.HMSModels;

[Table("nurses")]
public partial class Nurse
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("gender")]
    [StringLength(20)]
    [Unicode(false)]
    public string Gender { get; set; } = null!;

    [Column("fk_dept")]
    public int? FkDept { get; set; }

    [Column("age")]
    public int Age { get; set; }

    [Column("email")]
    [StringLength(255)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("password")]
    [StringLength(255)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("phone")]
    [StringLength(11)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("address")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Address { get; set; }

    [Column("image")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Image { get; set; }

    [Column("fk_status")]
    public int? FkStatus { get; set; }

    [ForeignKey("FkDept")]
    [InverseProperty("Nurses")]
    public virtual Department? FkDeptNavigation { get; set; }

    [ForeignKey("FkStatus")]
    [InverseProperty("Nurses")]
    public virtual Status? FkStatusNavigation { get; set; }
}
