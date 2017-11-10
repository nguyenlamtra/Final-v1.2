using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.FrontEnd.Models;
using COmpStore.FrontEnd.Service.Admin;
using COmpStore.FrontEnd.Helper;

namespace COmpStore.FrontEnd.Controllers
{
    public class ProductController : Controller
    {
        private readonly IService<ProductModel> _productService;
        private readonly IService<PublisherModel> _publisherService;
        private readonly IService<SubCategoryModel> _subCategoryService;
        private readonly IService<CategoryModel> _categoryService;

        public ProductController(IService<ProductModel> productService, IService<SubCategoryModel> subCategoryService,
            IService<PublisherModel> publisherService, IService<CategoryModel> categoryService)
        {
            _productService = productService;
            _publisherService = publisherService;
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult SaveProduct(int productId)
        {
            var selectedProducts = HttpContext.Session.GetSelectedProducts();
            selectedProducts.Add(new SelectedProduct { ProductId = productId, Quantity = 1 });
            HttpContext.Session.SetSelectedProducts(selectedProducts);
            return Json(true);
        }
        
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();
            ViewData["categories"] =await _categoryService.GetAll();
            ViewData["publishers"] =await _publisherService.GetAll();

            return View(products);
        }
    }
}