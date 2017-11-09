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
using COmpStore.FrontEnd.Service.Admin;

namespace COmpStore.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<ProductModel> _productService;
        private readonly IService<PublisherModel> _publisherService;
        private readonly IService<SubCategoryModel> _subCategoryService;
        private readonly IService<CategoryModel> _categoryService;
       

        public HomeController(IService<ProductModel> productService,
                                IService<SubCategoryModel> subCategoryService,
                                IService<PublisherModel> publisherService,
                                IService<CategoryModel> categoryService)
        {
            _productService = productService;
            _publisherService = publisherService;
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["categories"] = await _categoryService.GetAll();
            ViewData["publishers"] = await _publisherService.GetAll();
            var products = await _productService.GetAll();
            return View(products);
        }


        public async  Task<IActionResult> SlideBarMenu()
        {
            ViewData["categories"] = await _categoryService.GetAll();
            ViewData["publishers"] = await _publisherService.GetAll();
            return View();
        }
       

        public async Task<IActionResult> SearchBySubCategory(int id)
        {
            
            var subCategory = await _subCategoryService.GetById(id);
            var products = await _productService.GetAll();
            ViewData["categories"] = await _categoryService.GetAll();
            ViewData["publishers"] = await _publisherService.GetAll();
            var items = products.Where(p => p.SubCategoryId == id).OrderBy(p => p.Id).ToList();
            ViewBag.Products = items;
            return View("Product");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
