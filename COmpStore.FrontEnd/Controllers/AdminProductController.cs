using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.FrontEnd.Models;
using COmpStore.FrontEnd.Service.Admin;
using COmpStore.FrontEnd.Service;
using COmpStore.FrontEnd.Const;
using COmpStore.FrontEnd.Helper;

namespace COmpStore.FrontEnd.Controllers
{
    public class AdminProductController : Controller
    {
        private readonly IService<ProductModel> _productService;
        private readonly IService<PublisherModel> _publisherService;
        private readonly IService<SubCategoryModel> _subCategoryService;

        public AdminProductController(IService<ProductModel> productService, IService<SubCategoryModel> subCategoryService, IService<PublisherModel> publisherService)
        {
            _productService = productService;
            _publisherService = publisherService;
            _subCategoryService = subCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();
            foreach (var product in products)
            {
                product.Image = WebCommon.API_IMAGE_URL + product.Image;
            }
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _productService.GetById(id));
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
            ViewBag.SubCategories = await _subCategoryService.GetAll();
            ViewBag.Publishers = await _publisherService.GetAll();
            var result = await _productService.Create(model);
            if (result != null)
            {
                ViewBag.IsSuccess = true;
                return View(result);
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetById(id);

            ViewBag.SubCategories = await _subCategoryService.GetAll();
            ViewBag.Publishers = await _publisherService.GetAll();
            ViewBag.SubCategoryId = product.SubCategoryId;
            ViewBag.PublisherId = product.PublisherId;

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductModel model)
        {
            ViewBag.SubCategories = await _subCategoryService.GetAll();
            ViewBag.Publishers = await _publisherService.GetAll();
            ViewBag.SubCategoryId = model.SubCategoryId;
            ViewBag.PublisherId = model.PublisherId;

            var result = await _productService.Update(model);
            if (result != null)
            {
                ViewBag.IsSuccess = true;
                return View(result);
            }
            else
                return View(model);
        }

        [HttpPost]
        public async Task<bool> Delete(int[] ids)
        {
            if (await _productService.Delete(ids))
                return true;
            else
                return false;
        }
    }
}