using System;
using System.Collections.Generic;

namespace OA_Example_Project.Models;

public partial class Patient
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int HospitalId { get; set; }

    public DateOnly? Dob { get; set; }

    public virtual Hospital Hospital { get; set; } = null!;

    public virtual ICollection<MedicalTransaction> MedicalTransactions { get; set; } = new List<MedicalTransaction>();
}
