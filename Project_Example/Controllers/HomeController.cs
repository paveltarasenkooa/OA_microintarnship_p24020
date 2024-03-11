using Microsoft.AspNetCore.Mvc;
using OA_Example_Project.Models;

namespace OA_Example_Project.Controllers
{
    public class HomeController : Controller
    {

        private readonly OaProjectContext _context;

        public HomeController(OaProjectContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Table()
        {
            return View();
        }
    }
}
