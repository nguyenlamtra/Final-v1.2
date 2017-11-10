using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.FrontEnd.Models;
using COmpStore.FrontEnd.Helper;
using COmpStore.FrontEnd.Service.User;

namespace COmpStore.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        // public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            //var temp = await CategoryService.GetList();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
