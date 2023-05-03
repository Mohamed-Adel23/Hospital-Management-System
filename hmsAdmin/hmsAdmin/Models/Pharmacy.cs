using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.Models;

[Table("pharmacy")]
public partial class Pharmacy
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("med_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string MedName { get; set; } = null!;

    [Column("cost")]
    public int? Cost { get; set; }

    [Column("amount")]
    public int? Amount { get; set; }
}
