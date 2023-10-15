using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBackAPI.Models;

namespace MovieBackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ActorController : ControllerBase
    {
        private readonly Context dbContext;

        public ActorController(Context context)
        {
            this.dbContext = context;
        }


        /// <summary>
        /// Returns the list of actors (Id, Name)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {

            var actors = dbContext.Actors.ToList();

            var actorsDto = new List<AllDirectorDTO>();

            foreach (var actor in actors)
            {
                actorsDto.Add(new AllDirectorDTO()
                {
                    Id = actor.Id,
                    Name = actor.Name
                });
            }
            
            return Ok(actorsDto);
        }

        /// <summary>
        /// Get the detail of one actor and the movies he played in (Id, Name, Biography, DateOfBirth, MoviesID, MoviesName, ActorRole )
        /// </summary>
        /// <param name="id">Id of the the actor</param>
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var actor = dbContext.Actors
                .Include(actor => actor.ActedMovies)
                .ThenInclude(actedMovie => actedMovie.Movie)
                .FirstOrDefault(actor => actor.Id == id);

            if (actor == null) {
                return NotFound(); 
            }

            var actorDto = new DetailActorDTO();

            actorDto.Id = id;
            actorDto.Name = actor.Name;
            actorDto.DateOfBirth = actor.DateOfBirth;
            actorDto.Biography = actor.Biography;

            actorDto.Movies = new List<ActorMovieDTO>();

            foreach(ActorMovie actedMovie in actor.ActedMovies)
            {
                actorDto.Movies.Add(new ActorMovieDTO()
                {
                    MovieId = actedMovie.MovieId,
                    MovieName = actedMovie.Movie.Title,
                    Role = actedMovie.Role

                });
            }            

            return Ok(actorDto);
        }

        /// <summary>
        /// Create an actor with a Name, a Biography and a DateOfBirth
        /// </summary>
        [HttpPost(Name = "AddActor")]
        public IActionResult Create([FromBody] AddActorDTO actor)
        {
            var newActor = new Actor
            {
                Biography = actor.Biography,
                DateOfBirth = actor.DateOfBirth,
                Name = actor.Name  
            };

            dbContext.Actors.Add(newActor);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newActor.Id }, actor);
        }

        /// <summary>
        /// Update actor information (Name, Biography, DateOfBirth)
        /// </summary>
        [HttpPut(Name = "UpdateActor")]
        public IActionResult Update([FromBody] UpdateActorDTO actor)
        {
            var updatedActor = dbContext.Actors
                .Include(actor => actor.ActedMovies)
                .FirstOrDefault(act => act.Id == actor.Id);

            if (updatedActor == null) {
                return NotFound();
            }

            updatedActor.Biography = actor.Biography;
            updatedActor.DateOfBirth = actor.DateOfBirth;
            updatedActor.Name = actor.Name;

            dbContext.Actors.Update(updatedActor);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = updatedActor.Id }, actor);
        }

        /// <summary>
        /// Delete an actor
        /// </summary>
        /// <param name="id">Id of the the actor</param>
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var actor = dbContext.Actors.Find(id);

            if (actor == null) 
            {
                return NotFound();
            }

            dbContext.Actors.Remove(actor);
            dbContext.SaveChanges();

            return Ok("The actor has been deleted.");
        }
    }
}