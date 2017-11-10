using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using COmpStore.Dto;
using COmpStore.Repositories;
using COmpStore.Models;
using COmpStore.Schema.Entities;

namespace COmpStore.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost("get-cart")]
        public IActionResult GetForCartView([FromBody]int[] productIds)
        {
            return Ok(Mapper.Map<IEnumerable<CartDto>>(_productRepository.GetMany(productIds)));
        }

        [HttpPost("save-order")]
        public IActionResult SaveOrder([FromBody]OrderViewModel orderViewModel)
        {
            var orderDetails = Mapper.Map<IEnumerable<OrderDetail>>(orderViewModel.SelectedProducts);
            Order order = new Order
            {
                Address = orderViewModel.Address,
                CreateDate = DateTime.Now,
                OrderDetails = orderDetails,
                Phone = orderViewModel.Phone,
                UserId = orderViewModel.UserId
            };
            if (_orderRepository.Add(order))
                return Ok();
            else
                return BadRequest();

        }
    }
}