using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.Services;
using COmpStore.Schema.Entities;
using COmpStore.Dto;
using AutoMapper;

namespace COmpStore.Controllers
{
    [Route("api/[controller]")]
    public class SubCategoryController : Controller
    {
        private ISubCategoryRepository _subCategoryRepository;

        public SubCategoryController(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        [HttpGet]
        //  [ProducesResponseType(typeof(List<SubCategory>), 200)]
        public IActionResult GetAllSubCategory()
        {
            var allSubCategoryRepository = _subCategoryRepository.GetAllSubCategories().ToList();

            var allSubCategoryDto = allSubCategoryRepository.Select(x => Mapper.Map<SubCategoryDto>(x));

            return Ok(allSubCategoryDto);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingleSubCategory(int id)
        {
            SubCategory subCategory = _subCategoryRepository.GetSingleSubCategory(id);

            if (subCategory == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<SubCategoryDto>(subCategory));
        }

        // POST api/customers

        [HttpPost]
        //[ProducesResponseType(typeof(CategoryDto), 201)]
        //[ProducesResponseType(typeof(CategoryDto), 400)]
        public IActionResult AddSubCategory([FromBody] SubCategoryDto subCategoryDto)
        {
            if (subCategoryDto == null)
            {
                return BadRequest("categorycreate object was null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SubCategory toAdd = Mapper.Map<SubCategory>(subCategoryDto);

            _subCategoryRepository.Add(toAdd);

            bool result = _subCategoryRepository.Save();

            if (!result)
            {
                //return new StatusCodeResult(500);
                throw new Exception("something went wrong when adding a new subcategory");
            }

            //return Ok(Mapper.Map<CategoryDto>(toAdd));
            return CreatedAtRoute("GetSingleSubCategory", new { id = toAdd.Id }, Mapper.Map<SubCategoryDto>(toAdd));
        }

        // PUT api/customers/{id}

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateSubCategory(int id, [FromBody] SubCategoryDto subCategoryDto)
        {
            if (subCategoryDto == null)
            {
                return BadRequest();
            }
            var existingSubCategory = _subCategoryRepository.GetSingleSubCategory(id);

            subCategoryDto.Id = existingSubCategory.Id;
            if (existingSubCategory == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(subCategoryDto, existingSubCategory);

            _subCategoryRepository.Update(existingSubCategory);

            bool result = _subCategoryRepository.Save();

            if (!result)
            {
                //return new StatusCodeResult(500);
                throw new Exception($"something went wrong when updating the subcategory with id: {id}");
            }

            return Ok(Mapper.Map<SubCategoryDto>(existingSubCategory));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Remove(int id)
        {
            var existingSubCategory = _subCategoryRepository.GetSingleSubCategory(id);

            if (existingSubCategory == null)
            {
                return NotFound();
            }

            _subCategoryRepository.Delete(id);

            bool result = _subCategoryRepository.Save();

            if (!result)
            {
                // return new StatusCodeResult(500);
                throw new Exception($"something went wrong when deleting the subcategory with id: {id}");
            }

            return NoContent();
        }
    }
}
