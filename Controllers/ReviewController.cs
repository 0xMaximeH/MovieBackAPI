using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBackAPI.Models;

namespace MovieBackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReviewController : ControllerBase
    {
        private readonly Context dbContext;

        public ReviewController(Context context)
        {
            this.dbContext = context;
        }


        /// <summary>
        /// Get the detail of one user (Id, Name, Favorites Movies, Reviews, Followers and following count)
        /// </summary>
        /// <param name="id">Id of the user</param>
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetFavoritesMovies([FromRoute] int id)
        {
            var user = dbContext.Users
                .Select(u => new DetailUserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Reviews = u.Reviews,
                    FavoriteMovies = u.FavoriteMovies
                })
                .FirstOrDefault(user => user.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateReview([FromBody] ReviewDTO reviewDTO)
        {
            string userId = HttpContext.User.FindFirst("userId").Value;
            var id = Convert.ToInt32(userId);

            var movie = dbContext.Movies.Find(reviewDTO.MovieId);
            if(movie == null)
            {
                return NotFound("Cannot create a review on a movie that do not exist");
            }

            dbContext.Reviews.Add(new Review
            {
                UserId = id,
                MovieId = reviewDTO.MovieId,
                Rate = reviewDTO.Rate,
                Comment = reviewDTO.Comment
            });

            dbContext.SaveChanges();

            return Ok();
        }

    }
}