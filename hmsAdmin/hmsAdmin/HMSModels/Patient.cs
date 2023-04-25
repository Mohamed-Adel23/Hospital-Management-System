using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.HMSModels;

[Table("patients")]
[Index("Nid", Name = "UQ__patients__C7DEC332A91E8FE2", IsUnique = true)]
public partial class Patient
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("NID")]
    public long Nid { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("gender")]
    [StringLength(20)]
    [Unicode(false)]
    public string Gender { get; set; } = null!;

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

    [InverseProperty("FkPatNavigation")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
