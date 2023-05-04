using System;
using System.Collections.Generic;

namespace HMSproject.Models;

public partial class Pharmacy
{
    public int Id { get; set; }

    public string MedName { get; set; } = null!;

    public int? Cost { get; set; }

    public int? Amount { get; set; }
}
