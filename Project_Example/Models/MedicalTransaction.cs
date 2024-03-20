using System;
using System.Collections.Generic;

namespace OA_Example_Project.Models;

public partial class MedicalTransaction
{
    public int Id { get; set; }

    public string RxNumber { get; set; } = null!;

    public string Ndc { get; set; } = null!;

    public decimal Quantity { get; set; }

    public int? DaysSupply { get; set; }

    public decimal? TotalPrice { get; set; }

    public int PatientId { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
