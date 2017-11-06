using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.FrontEnd.Service.Admin;
using COmpStore.FrontEnd.Models;

namespace COmpStore.FrontEnd.Controllers
{
    public class AdminCategoryController : Controller
    {
        private IService<CategoryModel> _categoryService;

        public AdminCategoryController(IService<CategoryModel> service)
        {
            _categoryService = service;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAll();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel model)
        {
            var result = await _categoryService.Create(model);
            if (result)
            {
                ViewBag.IsSuccess = true;
                return View();
            }
                
            else
                return View();
        }

        public async Task<IActionResult> Details(int categoryId)
        {
            var result = await _categoryService.GetById(categoryId);
            return View(result);
        }

        public async Task<IActionResult> Update(int categoryId)
        {
            return View(await _categoryService.GetById(categoryId));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryModel model)
        {
            var result = await _categoryService.Update(model);
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