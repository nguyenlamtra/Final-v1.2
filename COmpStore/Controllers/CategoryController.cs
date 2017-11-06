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

namespace COmpStore.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryService)
        {
            _categoryRepository = categoryService;
        }

        [HttpGet("{categoryId}")]
        public IActionResult Get(int categoryId)
        {
            var category = _categoryRepository.GetById(categoryId);
            if (category != null)
            {
                var list = new List<CategoryDto>();
                list.Add(category);
                return Ok(list);
            }
                
            else
                return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _categoryRepository.GetAll();

            return Ok(categories);
        }

        [HttpPut]
        public IActionResult Put([FromBody] CategoryDto dto)
        {
            if (_categoryRepository.Update(dto))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(bool), 400)]
        public IActionResult Create([FromBody] CategoryDto dto)
        {
            if (dto == null || dto.Id != 0)
            {
                return BadRequest();
            }

            if (_categoryRepository.Create(dto))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int categoryId)
        {

            if (_categoryRepository.Delete(categoryId))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}