using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.FrontEnd.Service.Admin;
using COmpStore.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            if (result.GetType()==typeof(CategoryModel))
            {
                ViewBag.IsSuccess = true;
                return View((CategoryModel)result);
            }
            else
            {
                var modelState = (ModelStateDictionary)result;
                foreach (var error in modelState.Values)
                {
                }
                return View(model);
            }
                
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var result = await _categoryService.GetById(id);
            return View(result);
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _categoryService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryModel model)
        {
            var result = await _categoryService.Update(model);
            if (result.GetType()==typeof(CategoryModel))
            {
                ViewBag.IsSuccess = true;
                return View((CategoryModel)result);
            }
            else
                return View();
        }

        [HttpPost]
        public async Task<bool> Delete(int[] ids)
        {
            if (await _categoryService.Delete(ids))
                return true;
            else
                return false;
        }
    }
}