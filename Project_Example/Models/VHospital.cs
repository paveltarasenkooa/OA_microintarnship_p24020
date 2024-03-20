using System;
using System.Collections.Generic;

namespace OA_Example_Project.Models;

public partial class VHospital
{
    public int HositalId { get; set; }

    public string HospitalName { get; set; } = null!;

    public string HospitalType { get; set; } = null!;

    public DateOnly? Maxdob { get; set; }

    public int? PatientCount { get; set; }
}
