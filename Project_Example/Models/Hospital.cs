using System;
using System.Collections.Generic;

namespace OA_Example_Project.Models;

public partial class Hospital
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly OpeningDate { get; set; }

    public int HopitalTypeId { get; set; }

    public string? ContactPhone { get; set; }

    public virtual HospitalType HopitalType { get; set; } = null!;

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
