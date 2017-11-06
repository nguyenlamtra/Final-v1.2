using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.FrontEnd.Service.Admin;
using COmpStore.FrontEnd.Models;

namespace COmpStore.FrontEnd.Controllers
{
    public class AdminSubCategoryController : Controller
    {
        IService<SubCategoryModel> _subCategoryService;

        public AdminSubCategoryController(IService<SubCategoryModel> subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _subCategoryService.GetAll();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryModel model)
        {
            var result = await _subCategoryService.Create(model);
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
            var result = await _subCategoryService.GetById(id);
            return View(result);
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _subCategoryService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(SubCategoryModel model)
        {
            var result = await _subCategoryService.Update(model);
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