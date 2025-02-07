
using Microsoft.AspNetCore.Mvc;

namespace MadeInCanadaForum.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {

        }

        //Home page
        public IActionResult Index()
        {
            //perform the work
            return View();
        }


        //Privacy page
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
