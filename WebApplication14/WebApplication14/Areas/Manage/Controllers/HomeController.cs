using Microsoft.AspNetCore.Mvc;
using WebApplication14.DAL;

namespace WebApplication14.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HomeController : Controller
    {
     
        public IActionResult Index()
        {
            return View();
        }
    }
}
