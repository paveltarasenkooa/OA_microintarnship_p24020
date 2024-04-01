
using System;
using System.Collections.Generic;

namespace OA_Example_Project.Models
{
    public class VPatient
    {
        public int PatientId { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string HospitalName { get; set; } = null!;
        public int HospitalId { get; set; }
       public int? MedicalTransactionsCount { get; set; }
    }
}
