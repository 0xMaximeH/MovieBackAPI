using Microsoft.AspNetCore.Mvc;
using MovieBackAPI.Models;

namespace MovieBackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorController : ControllerBase
    {
        private readonly Context dbContext;

        public DirectorController(Context context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var directors = dbContext.Directors.ToList();
            return Ok(directors);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var director = dbContext.Directors.Find(id);

            if(director == null) {
                return NotFound(); 
            }

            return Ok(director);
        }

        [HttpPost(Name = "AddDirector")]
        public IActionResult Create([FromBody] Director director)
        {
            var newDirector = new Director
            {
                Biography = director.Biography,
                DateOfBirth = DateTime.Now,
                Name = director.Name  
            };
            
            dbContext.Directors.Add(newDirector);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newDirector.Id }, newDirector);
        }

        [HttpPut(Name = "UpdateDirector")]
        public IActionResult Update([FromBody] Director director)
        {
            var updatedDirector = dbContext.Directors.Find(director.Id);

            if (updatedDirector == null) {
                return NotFound();
            }

            updatedDirector.Biography = director.Biography;
            updatedDirector.DateOfBirth = DateTime.Now;
            updatedDirector.Name = director.Name;

            dbContext.Directors.Update(updatedDirector);

            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = updatedDirector.Id }, updatedDirector);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var director = dbContext.Directors.Find(id);

            if (director == null) {
                return NotFound();
            }

            dbContext.Directors.Remove(director);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}