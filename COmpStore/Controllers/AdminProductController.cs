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
using COmpStore.Helper;

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
        [ValidateModel]
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
                Product product = Mapper.Map<Product>(productDto);
                product.Image = Guid.NewGuid() + ".jpg";
                _productRepository.Add(product);
                if (!_productRepository.Save())
                {
                    throw new Exception("something went wrong when adding a new subcategory");
                }
                await FileHelper.AddFileAsync(filePath, base64String, product.Image);
               
                return Ok(productDto);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/customers/{id}

        [HttpPut]
        [ValidateFormAttribute]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            var product = Mapper.Map<Product>(productDto);
            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
            {
                _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            var filePath = _hostingEnvironment.WebRootPath + "\\images\\products";
            var base64String = Regex.Replace(productDto.Image, "^data:image\\/[a-zA-Z]+;base64,", String.Empty);
            if (base64String.IsBase64())
            {
                var imageName = _productRepository.GetImage(product.Id);
<<<<<<< HEAD
                if (imageName == "") 
=======
                if (imageName != null) 
>>>>>>> 1d0bfa58f7de88ebda201218ff4bf2506565f1b2
                    FileHelper.DeleteFile(filePath, imageName);
                product.Image = Guid.NewGuid() + ".jpg";
                _productRepository.Update(product);
                if (!_productRepository.Save())
                    throw new Exception("something went wrong when UpdateProduct");
                await FileHelper.AddFileAsync(filePath, base64String,product.Image);
                return Ok(product);
            }
            else
            {
                _productRepository.UpdateExceptImage(product);

                if (!_productRepository.Save())
                    throw new Exception("something went wrong when adding a new subcategory");
                else
                    return Ok(product);
            }
        }

        //[HttpDelete]
        //[Route("{id}")]
        //public IActionResult Remove(int id)
        //{
            
        //    var existingProduct = _productRepository.GetSingleProduct(id);

        //    if (existingProduct == null)
        //    {
        //        return NotFound();
        //    }

        //    _productRepository.Delete(id);

        //    bool result = _productRepository.Save();

        //    if (!result)
        //    {
        //        // return new StatusCodeResult(500);
        //        throw new Exception($"something went wrong when deleting the subcategory with id: {id}");
        //    }
            
        //    return NoContent();
        //}

        [HttpDelete]
        public IActionResult Delete([FromBody]int[] ids)
        {
            try
            {
                if (ids.Contains(0))
                {
                    return BadRequest();
                }
                var filePath = _hostingEnvironment.WebRootPath + "\\images\\products";
                foreach (var id in ids)
                {
                    var product = _productRepository.GetSingleProduct(id);
                    if (System.IO.File.Exists(Path.Combine(filePath, product.Image)))
                    {
                        try
                        {
                            System.IO.File.Delete(Path.Combine(filePath, product.Image));
                        }
                        catch (System.IO.IOException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    _productRepository.Delete(id);
                }
                _productRepository.Save();

                return NoContent();
            }
            catch (Exception )
            {
                return NotFound();
            }
        }
    }
}