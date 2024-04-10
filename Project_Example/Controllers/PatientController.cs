using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OA_Example_Project.Models;

namespace OA_Example_Project.Controllers
{
    public class PatientController : Controller
    {

        private readonly OaProjectContext _context;

        public PatientController(OaProjectContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            var hospitals = _context.Hospitals.Select(h => new
            {
                h.Id,
                h.Name
            }).ToList();

            ViewBag.Hospitals = new SelectList(hospitals, "Id", "Name");

            return View("PatientEdit",patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Patient patient)
        {
            var hospitals = _context.Hospitals.Select(h => new
            {
                h.Id,
                h.Name
            }).ToList();

            ViewBag.Hospitals = new SelectList(hospitals, "Id", "Name");

            if (patient.Id == null )
            {
                return NotFound();
            }
            var dbPatient = _context.Patients.Find(patient.Id);
            dbPatient.FirstName = patient.FirstName;
            dbPatient.LastName = patient.LastName;
            dbPatient.Dob = patient.Dob;
            dbPatient.HospitalId = patient.HospitalId;
            _context.Update(dbPatient);
            _context.SaveChanges();
            return View("PatientEdit",patient);
        }

        public IActionResult Add()
        {
            var hospitals = _context.Hospitals.Select(h => new
            {
                h.Id,
                h.Name
            }).ToList();

            ViewBag.Hospitals = new SelectList(hospitals, "Id", "Name");
            return View("PatientAdd");
        }


        [HttpPost]
        public async Task<IActionResult> Add(Patient patient)
        {

            _context.Patients.Add(patient);
            _context.SaveChanges();
            return View("PatientEdit", patient);
        }



        [HttpGet("api/patient/GetPatients")]
        public ActionResult<IEnumerable<VHospital>> GetPatients(int pageIndex = 0, int pageSize = 10, int? hospitalId = null)
        {
            var hospitalName = "";
            var patientsQuery = _context.VPatients.AsQueryable();

            if (hospitalId.HasValue)
            {
                patientsQuery = patientsQuery.Where(p => p.HospitalId == hospitalId.Value);
                hospitalName = _context.Hospitals.FirstOrDefault(x => x.Id == hospitalId.Value).Name;
            }

            var totalRecords = patientsQuery.Count();

            var patients = patientsQuery.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();


            var response = new
            {
                Data = patients,
                TotalRecords = totalRecords,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize),
                HospitalName = hospitalName
            };
            return Ok(response);
        }
    }
}
