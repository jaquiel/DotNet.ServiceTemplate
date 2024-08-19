using DotNet.ServiceTemplate.Domain;
using DotNet.ServiceTemplate.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace DotNet.ServiceTemplate.Presentation.Controllers
{
    [ApiVersion("2")]
    [Route("api/v{apiVersion}/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IService<Person> _service;

        public PersonsController(IService<Person> service)
        {
            _service = service;
        }

        [MapToApiVersion("2")]
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            var result = _service.GetAll();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [MapToApiVersion("2")]
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int? id, 
                                 [FromQuery] string? firstname = null,
                                 [FromQuery] string? lastname = null)
        {
            Expression<Func<Person, bool>> filter = person => 
                 (!person.Id.HasValue || person.Id.Equals(id)) &&
                 (string.IsNullOrEmpty(person.FirstName) || person.FirstName.Equals(firstname)) &&
                 (string.IsNullOrEmpty(person.LastName) || person.LastName.Equals(lastname));

            var result = _service.Get(filter);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.FirstOrDefault());
        }

        [MapToApiVersion("2")]
        [HttpPost]
        public IActionResult Post([FromBody] Person value)
        {
            var result = _service.Post(value);
            
            if (result == null)
            {
                return BadRequest();
            };
            
            return Ok(result);
        }

        [MapToApiVersion("2")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person value)
        {
            var result = _service.Put(id, value);
            if (result == null)
            {
                return BadRequest();
            }
            
            return Ok(result);
        }

        [MapToApiVersion("2")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        //[MapToApiVersion("1")]
        //[HttpPatch]
        //public IActionResult Patch()
        //{
        //    return NoContent();
        //}
    }
}
