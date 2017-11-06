using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.Repositories;
using COmpStore.Dto;

namespace COmpStore.Controllers
{
    [Route("api/[controller]")]
    public class PublisherController : Controller
    {
        private IPublisherRepository _publisherRepository;

        public PublisherController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var publisher = _publisherRepository.GetById(id);
            if (publisher != null)
                return Ok(publisher);
            else
                return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var publisher = _publisherRepository.GetAll();

            return Ok(publisher);
        }

        [HttpPut]
        public IActionResult Put([FromBody] PublisherDto dto)
        {
            if (_publisherRepository.Update(dto))
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
        public IActionResult Create([FromBody] PublisherDto dto)
        {
            if (dto == null || dto.Id != 0)
            {
                return BadRequest();
            }

            if (_publisherRepository.Create(dto))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            if (_publisherRepository.Delete(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}