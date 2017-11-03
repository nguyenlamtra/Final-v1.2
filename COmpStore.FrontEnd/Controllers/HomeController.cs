using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.FrontEnd.Models;
using COmpStore.FrontEnd.Builder;
using System.Net.Http;
using COmpStore.FrontEnd.Service;

namespace COmpStore.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View(CategoryService.GetList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
