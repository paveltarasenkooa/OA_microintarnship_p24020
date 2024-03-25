using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ActionResult<IEnumerable<VHospital>> GetHospitals(int pageIndex = 0, int pageSize = 10)
        {
            var totalRecords =  _context.VHospitals.Count();
            var hospitals = _context.VHospitals.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();
            var response = new
            {
                Data = hospitals,
                TotalRecords = totalRecords,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };
            return Ok(response);
        }
    }
}
