using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.FrontEnd.Models;
using COmpStore.FrontEnd.Services;
using COmpStore.FrontEnd.Service;

namespace COmpStore.FrontEnd.Controllers
{
    public class ProductController : Controller
    {
        private readonly IService<ProductModel> _productService;
        private readonly IService<PublisherModel> _publisherService;
        private readonly IService<SubCategoryModel> _subCategoryService;

        public ProductController(IService<ProductModel> productService,IService<SubCategoryModel> subCategoryService, IService<PublisherModel> publisherService)
        {
            _productService = productService;
            _publisherService = publisherService;
            _subCategoryService = subCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAll());
        }

        public async Task<IActionResult> Details(int productId)
        {
            return View(await _productService.GetById(productId));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.SubCategories = await _subCategoryService.GetAll();
            ViewBag.Publishers = await _publisherService.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel model)
        {
            if (await _productService.Create(model))
                return View();
            else
                return View();
        }

        public async Task<IActionResult> Update(int productId)
        {
            ViewBag.SubCategories = await _subCategoryService.GetAll();
            ViewBag.Publishers = await _publisherService.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductModel model)
        {
            if (await _productService.Update(model))
                return View();
            else
                return View();
        }

        public async Task<IActionResult> Delete(int productId)
        {
            if (await _productService.Delete(productId))
                return View();
            else
                return View();
        }
    }
}