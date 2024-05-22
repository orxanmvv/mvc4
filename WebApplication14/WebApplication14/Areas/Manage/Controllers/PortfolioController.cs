using Microsoft.AspNetCore.Mvc;
using WebApplication14.DAL;
using WebApplication14.Models;

namespace WebApplication14.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PortfolioController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;

        }


        public IActionResult Index()
        {

            return View(_appDbContext.Portfolios.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Portfolio portfolio)
        {
            if (!portfolio.ImgFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Yanlis daxil edilib");
                return View();
            }
            string path = _webHostEnvironment.WebRootPath + @"\Upload\manage\";
            string filename = Guid.NewGuid() + portfolio.ImgFile.FileName;
            using (FileStream fileStream = new FileStream(path + filename, FileMode.Create))
            {
                portfolio.ImgFile.CopyTo(fileStream);
            }
            portfolio.ImgUrl = filename;
            _appDbContext.Portfolios.Add(portfolio);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {

            var deleteItem = _appDbContext.Portfolios.FirstOrDefault(p => p.Id == id);
            if (deleteItem != null)
            {
                _appDbContext.Remove(deleteItem);
                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id) {
        var deleteItem= _appDbContext.Portfolios.FirstOrDefault(p=>p.Id == id);
            return View(deleteItem);
        }
        [HttpPost]
        public IActionResult Update( Portfolio portfolio) { 
        if(!ModelState.IsValid)
            {
                return View(portfolio);
            }
        if(portfolio.ImgFile != null)
            {
                string path = _webHostEnvironment.WebRootPath + @"\Upload\manage\";
                string filename = Guid.NewGuid() + portfolio.ImgFile.FileName;
                using (FileStream fileStream = new FileStream(path + filename, FileMode.Create))
                {
                    portfolio.ImgFile.CopyTo(fileStream);
                }
                portfolio.ImgUrl = filename;

            }
          _appDbContext.Portfolios.Update(portfolio);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        
        
        }
    }
}
