using Microsoft.AspNetCore.Mvc;
using OA_Example_Project.Models;

namespace OA_Example_Project.Controllers
{
    [Route("api/patient")]
    [ApiController]
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


        [HttpGet]
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
