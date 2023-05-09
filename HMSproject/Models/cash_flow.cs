using System.ComponentModel.DataAnnotations;

namespace HMSproject.Models;

public class cash_flow
{
    [Key]
    public int id { get; set; }
    
    public int appointments_cash { get; set; }
    
    public int Lab_cash { get; set; }
    
    public int Pharmacy_cash { get; set; }
    
}