using System;
using System.Collections.Generic;

namespace OA_Example_Project.Models;

public partial class HospitalType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Hospital> Hospitals { get; set; } = new List<Hospital>();
}
