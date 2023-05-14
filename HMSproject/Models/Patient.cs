using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HMSproject.Models;

public partial class Patient:IdentityUser
{

    public string? SSN { get; set; }

    [PersonalData,MaxLength(100)]
    public string? Name { get; set; } = null!;

    public string? Gender { get; set; } = null!;

    public int? Age { get; set; }
    public int? Condition { get; set; }

    public string? Address { get; set; }

    
    [PersonalData]
    public byte[]? profilePic { get; set; }

    public virtual ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
    public Patient()
    {
        this.Condition = 0;
    }
}