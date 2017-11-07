using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.Services;
using COmpStore.Schema.Entities;
using COmpStore.Dto;
using Microsoft.AspNetCore.Authorization;
using COmpStore.Repositories;
using TokenAuthWebApiCore.Server.Filters;

namespace COmpStore.Controllers
{
    [Route("api/[controller]")]
    public class AdminCategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;

        public AdminCategoryController(ICategoryRepository categoryService)
        {
            _categoryRepository = categoryService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category != null)
                return Ok(category);
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _categoryRepository.GetAll();
            return Ok(categories);
        }

        [HttpPut]
        [ValidateFormAttribute]
        public IActionResult Put([FromBody] CategoryDto dto)
        {
            if (_categoryRepository.Update(dto))
                return Ok(dto);
            else
                return NotFound();
        }

        [HttpPost]
        [ValidateFormAttribute]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(bool), 400)]
        public IActionResult Create([FromBody] CategoryDto dto)
        {
            if (dto == null || dto.Id != 0)
                return BadRequest();

            if (_categoryRepository.Create(dto))
                return Ok(dto);
            else
                return BadRequest();
        }

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    if (_categoryRepository.Delete(id))
        //        return NoContent();
        //    else
        //        return NotFound();
        //}

        [HttpDelete]
        public IActionResult Delete([FromBody]int[] ids)
        {
            if (_categoryRepository.Delete(ids))
                return NoContent();
            else
                return NotFound();
        }
    }
}