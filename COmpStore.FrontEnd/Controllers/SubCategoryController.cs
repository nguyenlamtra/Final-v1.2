using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.FrontEnd.Service;
using COmpStore.FrontEnd.Models;

namespace COmpStore.FrontEnd.Controllers
{
    public class SubCategoryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var result = await SubCategoryService.GetAll();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryModel model)
        {
            var result = await SubCategoryService.Create(model);
            if (result)
            {
                ViewBag.IsSuccess = true;
                return View();
            }

            else
                return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await SubCategoryService.GetById(id);
            return View(result);
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await SubCategoryService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(SubCategoryModel model)
        {
            var result = await SubCategoryService.Update(model);
            if (result)
            {
                ViewBag.IsSuccess = true;
                return View();
            }
            else
                return View();
        }


    }
}