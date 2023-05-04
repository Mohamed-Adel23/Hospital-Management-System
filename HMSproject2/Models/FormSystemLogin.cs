using System.ComponentModel.DataAnnotations;

namespace HMSproject.Models;

public class FormSystemLogin
{
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Type { get; set; }
}