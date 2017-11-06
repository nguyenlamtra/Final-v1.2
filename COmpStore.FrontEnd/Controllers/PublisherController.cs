using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.FrontEnd.Service.Admin;
using COmpStore.FrontEnd.Models;

namespace COmpStore.FrontEnd.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IService<PublisherModel> _publisherService;

        public PublisherController(IService<PublisherModel> service)
        {
            _publisherService = service;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _publisherService.GetAll();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PublisherModel model)
        {
            var result = await _publisherService.Create(model);
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
            var result = await _publisherService.GetById(id);
            return View(result);
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _publisherService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(PublisherModel model)
        {
            var result = await _publisherService.Update(model);
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