using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBackAPI.Models;

namespace MovieBackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        private readonly Context dbContext;

        public ActorController(Context context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var actors = dbContext.Actors
                            .Include(actor => actor.ActedMovies)
                            .ToList();
            return Ok(actors);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var actor = dbContext.Actors.Find(id);

            if(actor == null) {
                return NotFound(); 
            }

            return Ok(actor);
        }

        [HttpPost(Name = "AddActor")]
        public IActionResult Create([FromBody] Actor actor)
        {
            var newActor = new Actor
            {
                Biography = actor.Biography,
                DateOfBirth = DateTime.Now,
                Name = actor.Name  
            };

            dbContext.Actors.Add(newActor);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newActor.Id }, newActor);
        }

        [HttpPut(Name = "UpdateActor")]
        public IActionResult Update([FromBody] Actor actor)
        {
            var updatedActor = dbContext.Actors.Find(actor.Id);

            if (updatedActor == null) {
                return NotFound();
            }

            updatedActor.Biography = updatedActor.Biography;
            updatedActor.DateOfBirth = DateTime.Now;
            updatedActor.Name = updatedActor.Name;

            dbContext.Actors.Update(updatedActor);

            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = updatedActor.Id }, updatedActor);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromBody] int id)
        {
            var actor = dbContext.Actors.Find(id);

            if (actor == null) {
                return NotFound();
            }

            dbContext.Actors.Remove(actor);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}