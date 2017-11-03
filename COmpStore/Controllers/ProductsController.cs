using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.Repositories;
using AutoMapper;
using COmpStore.Dto;
using COmpStore.Schema.Entities;

namespace COmpStore.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        //  [ProducesResponseType(typeof(List<Product>), 200)]
        public IActionResult GetAllProducts()
        {
            var allProductRepository = _productRepository.GetAllProducts().ToList();

            var allProductDto = allProductRepository.Select(x => Mapper.Map<ProductDto>(x));

            return Ok(allProductDto);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSingleProduct")]
        public IActionResult GetSingleProduct(int id)
        {
            Product product = _productRepository.GetSingleProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ProductDto>(product));
        }

        //    // POST api/customers

        [HttpPost]
        //[ProducesResponseType(typeof(CategoryDto), 201)]
        //[ProducesResponseType(typeof(CategoryDto), 400)]
        public IActionResult AddProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("categorycreate object was null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Product toAdd = Mapper.Map<Product>(productDto);

            _productRepository.Add(toAdd);

            bool result = _productRepository.Save();

            if (!result)
            {
                //return new StatusCodeResult(500);
                throw new Exception("something went wrong when adding a new subcategory");
            }

            //return Ok(Mapper.Map<ProductDto>(toAdd));
            return CreatedAtRoute("GetSingleProduct", new { id = toAdd.Id }, Mapper.Map<ProductDto>(toAdd));
        }

        // PUT api/customers/{id}

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }
            var existingProduct = _productRepository.GetSingleProduct(id);

            productDto.Id = existingProduct.Id;
            if (existingProduct == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(productDto, existingProduct);

            _productRepository.Update(existingProduct);

            bool result = _productRepository.Save();

            if (!result)
            {
                //return new StatusCodeResult(500);
                throw new Exception($"something went wrong when updating the subcategory with id: {id}");
            }

            return Ok(Mapper.Map<ProductDto>(existingProduct));
        }

          [HttpDelete]
        [Route("{id}")]
        public IActionResult Remove(int id)
        {
            var existingProduct = _productRepository.GetSingleProduct(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            _productRepository.Delete(id);

            bool result = _productRepository.Save();

            if (!result)
            {
                // return new StatusCodeResult(500);
                throw new Exception($"something went wrong when deleting the subcategory with id: {id}");
            }

            return NoContent();
        }
    }
}