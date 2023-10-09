using BecomeCaleb_WEB.Migration;
using BecomeCaleb_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BecomeCaleb_WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using(var db = new CalebContext())
            {
                var temp = db._TCDiaries.ToList();
                foreach(var tempItem in temp)
                {
                    Console.WriteLine(tempItem.Record);
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}