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
        /// Must be connected Get last 10 reviews of following
        /// </summary>
        [Authorize]
        [HttpGet]
        public IActionResult GetLastReviews()
        {
            string userId = HttpContext.User.FindFirst("userId").Value;
            var id = Convert.ToInt32(userId);

            var reviews = dbContext.Reviews
                .Where(r => r.User.Followers
                .Any(u => u.Id == id))
                .Select(a => new
                {
                    a.Id,
                    a.Rate,
                    a.Comment,
                    a.CreationDate,
                    FollowingId = a.UserId,
                    FollowingName = a.User.Name,
                    MovieName = a.Movie.Title,
                    a.MovieId
                })
                .OrderByDescending(e => e.CreationDate)
                .Take(10)
                .ToList();

            if (reviews == null)
            {
                return NotFound("No reviews or no following found");
            }

            return Ok(reviews);
        }

        /// <summary>
        /// Must be connected. Create a review of a movie with a rating and a comment.
        /// </summary>
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
                Comment = reviewDTO.Comment,
                CreationDate = DateTime.Now
            });

            dbContext.SaveChanges();

            return Ok();
        }

    }
}