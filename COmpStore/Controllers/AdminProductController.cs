using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.Repositories;
using AutoMapper;
using COmpStore.Dto;
using COmpStore.Schema.Entities;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using COmpStore.Extension;
using System.IO;
using TokenAuthWebApiCore.Server.Filters;

namespace COmpStore.Controllers
{
    [Route("api/[controller]")]
    public class AdminProductController : Controller
    {
        private IProductRepository _productRepository;
        private IHostingEnvironment _hostingEnvironment;

        public AdminProductController(IProductRepository productRepository,IHostingEnvironment hostingEnvironment)
        {
            _productRepository = productRepository;
            _hostingEnvironment = hostingEnvironment;
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
        [Route("{id}")]
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
        [ValidateFormAttribute]
        //[ProducesResponseType(typeof(CategoryDto), 201)]
        //[ProducesResponseType(typeof(CategoryDto), 400)]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
            {
                _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            var filePath = _hostingEnvironment.WebRootPath + "\\images\\products";
            var base64String = Regex.Replace(productDto.Image, "^data:image\\/[a-zA-Z]+;base64,", String.Empty);
            if (base64String.IsBase64())
            {
                Product toAdd = Mapper.Map<Product>(productDto);

                _productRepository.Add(toAdd);
                if (!_productRepository.Save())
                {
                    //return new StatusCodeResult(500);
                    throw new Exception("something went wrong when adding a new subcategory");
                }

                byte[] image = Convert.FromBase64String(base64String);
                var fileName = Guid.NewGuid() + ".jpg";
                using (var stream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    await stream.WriteAsync(image, 0, image.Length);
                    stream.Flush();
                }
                
                
                //return Ok(Mapper.Map<ProductDto>(toAdd));
                return CreatedAtRoute("GetSingleProduct", new { id = toAdd.Id }, Mapper.Map<ProductDto>(toAdd));
            }
            else
            {
                return BadRequest();
            }
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