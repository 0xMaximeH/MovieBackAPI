using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBackAPI.Models;

namespace MovieBackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorMovieController : ControllerBase
    {
        private readonly Context dbContext;

        public ActorMovieController(Context context)
        {
            this.dbContext = context;
        }        

        [HttpPost(Name = "AddRole")]
        public IActionResult Create([FromBody] AddActorMovieDTO role)
        {
            var actormovie = new ActorMovie
            {
                MovieId = role.MovieId,
                ActorId = role.ActorId,
                Role = role.Role
            };

            dbContext.ActorsMovies.Add(actormovie);            
            
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] DeleteActorMovieDTO role)
        {
            var actorMovie = dbContext.ActorsMovies
                .FirstOrDefault(x => x.MovieId == role.MovieId && x.ActorId == role.ActorId);

            if (actorMovie == null) {
                return NotFound($"No role found for these ids.");
            }

            dbContext.ActorsMovies.Remove(actorMovie);
            dbContext.SaveChanges();

            return Ok($"The role (actor, movie: {role.ActorId}, {role.MovieId}) has been deleted.");
        }
    }
}