using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace COmpStore.FrontEnd.Controllers
{
    public class AdminOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}