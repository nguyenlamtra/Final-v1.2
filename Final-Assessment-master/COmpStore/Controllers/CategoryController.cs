using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.Services;
using COmpStore.Schema.Entities;
using COmpStore.Dto;
using Microsoft.AspNetCore.Authorization;

namespace COmpStore.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private ICategoryService CategoryService;

        public CategoryController(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            
            return Ok(1);
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var categories = CategoryService.GetAll();
            return Ok(categories);
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "CategoryName": "Category Name"
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly-created TodoItem</returns>
        /// <response code="201">Returns the newly-created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(typeof(bool), 201)]
        [ProducesResponseType(typeof(bool), 400)]
        public IActionResult Create([FromBody] CategoryDto dto)
        {
            if (dto == null || dto.Id != 0)
            {
                return BadRequest();
            }

            if(CategoryService.Create(dto))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }

            
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            if (CategoryService.Delete(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}