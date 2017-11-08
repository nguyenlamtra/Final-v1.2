using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.FrontEnd.Service.Admin;
using COmpStore.FrontEnd.Models;

namespace COmpStore.FrontEnd.Controllers
{
    public class AdminPublisherController : Controller
    {
        private readonly IService<PublisherModel> _publisherService;

        public AdminPublisherController(IService<PublisherModel> service)
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
            if (result!=null)
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
            if (await _publisherService.Delete(ids))
                return true;
            else
                return false;
        }

        //[HttpPost]
        //public async Task<bool> Delete(int id)
        //{
        //    if (await _publisherService.Delete(id))
        //        return true;
        //    else
        //        return false;
        //}
    }
}