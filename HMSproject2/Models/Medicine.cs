using System;
using System.Collections.Generic;

namespace HMSproject.Models;

public partial class Medicine
{
    public int id{get; set;}
    
    public int? FkDia { get; set; }

    public string? Name { get; set; }

    public virtual Diagnose? FkDiaNavigation { get; set; }
}
