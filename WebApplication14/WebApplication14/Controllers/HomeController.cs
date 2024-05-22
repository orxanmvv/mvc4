using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication14.DAL;

namespace WebApplication14.Controllers
{
    public class HomeController : Controller
    {

        AppDbContext appDbContextdbContext;
        public HomeController(AppDbContext appDbContextdbContext)
        {
            this.appDbContextdbContext = appDbContextdbContext;
        }

        public IActionResult Index()
        {
            return View(appDbContextdbContext.Portfolios.ToList());
        }




    }
}