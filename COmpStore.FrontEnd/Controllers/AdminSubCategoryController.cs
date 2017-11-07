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
        IService<CategoryModel> _categoryService;

        public AdminSubCategoryController(IService<SubCategoryModel> subCategoryService, IService<CategoryModel> categoryService)
        {
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _subCategoryService.GetAll();
            return View(result);
        }

        public IActionResult Create(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View(new SubCategoryModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryModel model)
        {
            ViewBag.CategoryId = model.CategoryId;
            if (await _subCategoryService.Create(model)!=null)
            {
                ViewBag.IsSuccess = true;
                return View(new SubCategoryModel());
            }
            else
                return View(new SubCategoryModel());
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
            if (result!=null)
            {
                ViewBag.IsSuccess = true;
                return View();
            }
            else
                return View();
        }

        [HttpPost]
        public async Task<bool> Delete(int[] ids)
        {
            if (await _subCategoryService.Delete(ids))
                return true;
            else
                return false;
        }

        //[HttpPost]
        //public async Task<bool> Delete(int id)
        //{
        //    if (await _subCategoryService.Delete(id))
        //        return true;
        //    else
        //        return false;
        //}
    }
}