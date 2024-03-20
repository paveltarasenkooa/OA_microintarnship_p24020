using Microsoft.AspNetCore.Mvc;
using OA_Example_Project.Models;

namespace OA_Example_Project.Controllers
{
    [Route("api/hospital")]
    [ApiController]
    public class HospitalController : Controller
    {

        private readonly OaProjectContext _context;

        public HospitalController(OaProjectContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult<IEnumerable<VHospital>> GetHospitals()
        {
            return _context.VHospitals.ToList();
        }
    }
}
