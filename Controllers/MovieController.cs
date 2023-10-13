using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBackAPI.Models;

namespace MovieBackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly Context dbContext;

        public MovieController(Context context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var movies = dbContext.Movies.ToList();
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var movie = dbContext.Movies.Include(movie => movie.Director).FirstOrDefault(x => x.Id == id);

            if(movie  == null) {
                return NotFound(); 
            }

            return Ok(movie);
        }

        [HttpPost(Name = "AddMovie")]
        public IActionResult Create([FromBody] Movie movie)
        {
            var newMovie = new Movie
            {
                Title = movie.Title,
                DirectorId = movie.DirectorId,
                RealeaseYear = movie.RealeaseYear,
                Description = movie.Description,
                Rating = movie.Rating

            };

            dbContext.Movies.Add(newMovie);
            dbContext.SaveChanges();

            foreach (ActorMovie actor in movie.Actors)
            {
                var newActorMovie = new ActorMovie
                {
                    MovieId = newMovie.Id,
                    ActorId = actor.ActorId
                };
                dbContext.ActorsMovies.Add(newActorMovie);
            }
            
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newMovie.Id }, newMovie);
        }

        [HttpPut]
        public IActionResult UpdateMovie([FromBody] Movie movie)
        {
            var updatedMovie = dbContext.Movies.Find(movie.Id);

            if (movie == null) {
                return NotFound();
            }

            updatedMovie.Title = movie.Title;
            updatedMovie.DirectorId = movie.DirectorId;
            updatedMovie.Description = movie.Description;
            updatedMovie.Rating = movie.Rating;
            updatedMovie.Actors = movie.Actors;

            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = updatedMovie.Id }, updatedMovie);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteMovie([FromRoute] int id)
        {
            var movie = dbContext.Movies.Find(id);

            if (movie == null) {
                return NotFound();
            }

            dbContext.Movies.Remove(movie);
            dbContext.SaveChanges();

            return Ok(movie);
        }
    }
}