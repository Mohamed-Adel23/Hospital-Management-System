using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.HMSModels;

[Table("payment")]
public partial class Payment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("method")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Method { get; set; }

    [InverseProperty("FkPayNavigation")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
