using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.FrontEnd.Models;
<<<<<<< HEAD
using COmpStore.FrontEnd.Helper;
using COmpStore.FrontEnd.Service.User;
=======
using COmpStore.FrontEnd.Builder;
using System.Net.Http;
using COmpStore.FrontEnd.Service;
using COmpStore.FrontEnd.Service.Admin;
>>>>>>> 1d0bfa58f7de88ebda201218ff4bf2506565f1b2

namespace COmpStore.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
<<<<<<< HEAD
        private IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        // public async Task<IActionResult> Index()
        public IActionResult Index()
=======
        private readonly IService<ProductModel> _productService;
        private readonly IService<PublisherModel> _publisherService;
        private readonly IService<SubCategoryModel> _subCategoryService;
        private readonly IService<CategoryModel> _categoryService;
       

        public HomeController(IService<ProductModel> productService,
                                IService<SubCategoryModel> subCategoryService,
                                IService<PublisherModel> publisherService,
                                IService<CategoryModel> categoryService)
>>>>>>> 1d0bfa58f7de88ebda201218ff4bf2506565f1b2
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var token = await _homeService.GetToken(viewModel);
            return View();
        }

        [HttpPost]
        public IActionResult ChangeQuantity([FromBody]SelectedProduct selectedProduct)
        {
            var selectedProducts = HttpContext.Session.GetSelectedProducts();
            if (selectedProduct.Quantity > 0)
            {
                var temp = selectedProducts.SingleOrDefault(x => x.ProductId == selectedProduct.ProductId);
                temp.Quantity = selectedProduct.Quantity;
                HttpContext.Session.SetSelectedProducts(selectedProducts);
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        public async Task<IActionResult> Cart()
        {
            var selectedProducts = HttpContext.Session.GetSelectedProducts();
            var viewModel = await _homeService.GetForCartView(selectedProducts.Select(x=>x.ProductId).ToArray());
            //int[] array = { 29, 30, 31 };
            //List<SelectedProduct> list = new List<SelectedProduct>();
            //list.Add(new SelectedProduct { ProductId = 29, Quantity = 1 });
            //list.Add(new SelectedProduct { ProductId = 30, Quantity = 12 });
            //list.Add(new SelectedProduct { ProductId = 31, Quantity = 13 });

            //list.OrderBy(x => x.ProductId);
            //HttpContext.Session.SetSelectedProducts(list);
            //var viewModel = await _homeService.GetForCartView(array);
            selectedProducts.OrderBy(x => x.ProductId);
            viewModel.OrderBy(x => x.ProductId);
            ViewBag.Products = viewModel;
            return View(selectedProducts);
        }

        [HttpPost]
        public IActionResult OrderInfo(List<SelectedProduct> selectedProducts)
        {
            HttpContext.Session.SetSelectedProducts(selectedProducts);
            return View();
        }

        public async Task<IActionResult> OrderInfo(OrderViewModel orderViewModel)
        {
            orderViewModel.SelectedProducts = HttpContext.Session.GetSelectedProducts();
            if (await _homeService.SaveOrder(orderViewModel))
            {
                ViewBag.OrderSuccess = true;
                return RedirectToAction("Index");
            }
            else 
                return View();
        }

        public IActionResult RemoveProductFromCart(int productId)
        {
            var selectedProducts = HttpContext.Session.GetSelectedProducts();
            var product = selectedProducts.SingleOrDefault(x => x.ProductId == productId);
            selectedProducts.Remove(product);
            HttpContext.Session.SetSelectedProducts(selectedProducts);
            return Json(true);
        }
    }
}
