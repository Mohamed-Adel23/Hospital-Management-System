using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HMSproject.Models;
using Microsoft.EntityFrameworkCore;

namespace HMSproject.Models;

[Table("doctors")]
public partial class Doctor
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Your name must be between 3 and 30 characters")]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("gender")]
    [StringLength(20)]
    [Unicode(false)]
    public string Gender { get; set; } = null!;

    [Required]
    [DisplayName("Specialization")]
    [Column("fk_dept")]
    public int? FkDept { get; set; }

    [Column("age")]
    [Range(20, 70, ErrorMessage = "Invalid Age, Try Again")]
    public int Age { get; set; }

    [Column("email")]
    [StringLength(255)]
    [DataType(DataType.EmailAddress)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("password")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password Length must be at least 6 with special characters like $%#_+,")]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("experience")]
    [Range(1, 40, ErrorMessage = "Invalid Experience Years, Try Again")]
    public int Experience { get; set; }

    [DisplayName("Personal Profile")]
    [Column("personal_profile")]
    [StringLength(200, MinimumLength = 10, ErrorMessage = "Personal Profile must be between 10 and 200 characters")]
    [Unicode(false)]
    public string PersonalProfile { get; set; } = null!;

    [Column("phone")]
    [DataType(DataType.PhoneNumber)]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number must be 11 characters")]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("address")]
    [StringLength(200, MinimumLength = 10, ErrorMessage = "Address must be between 10 and 200 characters")]
    [Unicode(false)]
    public string? Address { get; set; }

    [Column("image")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Image { get; set; }

    [ForeignKey("FkDept")]
    [InverseProperty("Doctors")]
    public virtual Department? FkDeptNavigation { get; set; }
}
