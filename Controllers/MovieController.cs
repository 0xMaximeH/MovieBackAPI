using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBackAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieBackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MovieController : ControllerBase
    {
        private readonly Context dbContext;

        public MovieController(Context context)
        {
            this.dbContext = context;
        }

        /// <summary>
        /// Returns the list of movies (Id, Title, RealeaseYear, Rating)
        /// </summary>
        [HttpGet]        
        public IActionResult GetAll()
        {
            var movies = dbContext.Movies.ToList();
            var moviesDTO = new List<AllMovieDTO>();
            foreach (var movie in movies)
            {
                moviesDTO.Add(new AllMovieDTO()
                {
                    Id = movie.Id,
                    Rating = movie.Rating,
                    RealeaseYear = movie.RealeaseYear,
                    Title = movie.Title
                });
            }
            return Ok(moviesDTO);
        }

        /// <summary>
        /// Returns the detail of a movie (Id, Title, RealeaseYear, Rating, Description, Director, Actors)
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var movie = dbContext.Movies
                .Include(movie => movie.Director)
                .Include(movie => movie.Actors)
                .ThenInclude(actor => actor.Actor)
                .FirstOrDefault(movie => movie.Id == id);

            if(movie  == null) {
                return NotFound($"No movie found for this id : {id}."); 
            }

            var detailMovieDTO = new DetailMovieDTO();
            detailMovieDTO.Id = id;
            detailMovieDTO.Title = movie.Title;
            detailMovieDTO.Description = movie.Description;
            detailMovieDTO.Rating = movie.Rating;

            detailMovieDTO.Director = new DirectorOfMovieDTO();
            detailMovieDTO.Director.Id = movie.Director.Id;
            detailMovieDTO.Director.Name = movie.Director.Name;

            detailMovieDTO.Actors = new List<ActorInMovieDTO>();
            foreach(var actor in movie.Actors)
            {
                detailMovieDTO.Actors.Add(new ActorInMovieDTO()
                {
                    Id = actor.ActorId,
                    Name = actor.Actor.Name,
                    Role = actor.Role
                });

            }

            return Ok(detailMovieDTO);
        }

        [HttpPost(Name = "AddMovie")]
        public IActionResult Create([FromBody] AddMovieDTO movie)
        {

            var director = dbContext.Directors.Find(movie.DirectorId);
            if (director == null)
                return BadRequest("This director does not exist");

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

            return CreatedAtAction(nameof(GetById), new { id = newMovie.Id }, newMovie);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateMovieDTO movie)
        {
            var updatedMovie = dbContext.Movies.Find(movie.Id);

            if (updatedMovie == null) {
                return NotFound($"No movie found for this id : {movie.Id}");
            }

            updatedMovie.Title = movie.Title;
            updatedMovie.DirectorId = movie.DirectorId;
            updatedMovie.Description = movie.Description;
            updatedMovie.RealeaseYear = movie.RealeaseYear;
            updatedMovie.Rating = movie.Rating;

            dbContext.Movies.Update(updatedMovie);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = updatedMovie.Id }, updatedMovie);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var movie = dbContext.Movies.Find(id);

            if (movie == null) {
                return NotFound($"No movie found for this id : {id}.");
            }

            dbContext.Movies.Remove(movie);
            dbContext.SaveChanges();

            return Ok($"The movie (id: {id}) has been deleted.");
        }
    }
}