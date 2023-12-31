using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBackAPI.Models;

namespace MovieBackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DirectorController : ControllerBase
    {
        private readonly Context dbContext;

        public DirectorController(Context context)
        {
            this.dbContext = context;
        }

        /// <summary>
        /// Returns the list of directors (Id, Name)
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var directors = dbContext.Directors.ToList();

            var directorsDTO = new List<AllDirectorDTO>();

            foreach (var director in directors)
            {
                directorsDTO.Add(new AllDirectorDTO()
                {
                    Id = director.Id,
                    Name = director.Name
                });
            }

            return Ok(directorsDTO);
        }

        /// <summary>
        /// Get the detail of one director and his movies (Id, Name, Biography, DateOfBirth, MoviesID, Movies Name )
        /// </summary>
        /// <param name="id">Id of the director</param>
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var director = dbContext.Directors
                .Include(director => director.DirectedMovies)
                .FirstOrDefault(director => director.Id == id);

            if (director == null) {
                return NotFound(); 
            }
            var directorDto = new DetailDirectorDTO();

            directorDto.Id = id;
            directorDto.Name = director.Name;
            directorDto.DateOfBirth = director.DateOfBirth;
            directorDto.Biography = director.Biography;

            directorDto.Movies = new List<DirectedMovieDTO>();

            foreach(Movie movie in director.DirectedMovies)
            {
                directorDto.Movies.Add(new DirectedMovieDTO()
                {
                    MovieId = movie.Id,
                    MovieName = movie.Title
                });
            }

            return Ok(directorDto);
        }

        /// <summary>
        /// Create a director with a Name, a Biography and a DateOfBirth
        /// </summary>
        [HttpPost(Name = "AddDirector")]
        public IActionResult Create([FromBody] AddDirectorDTO director)
        {
            var newDirector = new Director
            {
                Biography = director.Biography,
                DateOfBirth = director.DateOfBirth,
                Name = director.Name
            };
            
            dbContext.Directors.Add(newDirector);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newDirector.Id }, director);
        }

        /// <summary>
        /// Update director information (Name, Biography, DateOfBirth)
        /// </summary>
        [HttpPut(Name = "UpdateDirector")]
        public IActionResult Update([FromBody] UpdateDirectorDTO director)
        {
            var updatedDirector = dbContext.Directors.Find(director.Id);

            if (updatedDirector == null) {
                return NotFound();
            }

            updatedDirector.Biography = director.Biography;
            updatedDirector.DateOfBirth = director.DateOfBirth;
            updatedDirector.Name = director.Name;

            dbContext.Directors.Update(updatedDirector);

            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = updatedDirector.Id }, director);
        }

        /// <summary>
        /// Delete an actor
        /// </summary>
        /// <param name="id">Id of the director</param>
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var director = dbContext.Directors.Find(id);

            if (director == null) {
                return NotFound();
            }

            dbContext.Directors.Remove(director);
            dbContext.SaveChanges();

            return Ok("The director has been deleted.");
        }
    }
}