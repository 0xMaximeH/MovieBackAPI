using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBackAPI.Models;

namespace MovieBackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly Context dbContext;

        public UserController(Context context)
        {
            this.dbContext = context;
        }

        
        /// <summary>
        /// Returns the list of users (id, name, review count)
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = dbContext.Users
                .Select(u => new AllUserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    ReviewCount = u.Reviews.Count()
                })
                .ToList();
            
            return Ok(users);
        }

        /// <summary>
        /// Get the detail of one user (Id, Name, Favorites Movies, Reviews, Followers and following count)
        /// </summary>
        /// <param name="id">Id of the user</param>
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetDetailFromId([FromRoute] int id)
        {            
            var user = dbContext.Users
                .Select(u => new DetailUserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Reviews = u.Reviews,
                    FavoriteMovies = u.FavoriteMovies,
                    FollowersCount = u.Followers.Count(),
                    FollowingCount = u.Following.Count()
                })
                .FirstOrDefault(user => user.Id == id);

            if (user == null) {
                return NotFound(); 
            } 

            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddFavoriteMovies([FromBody] ChangeFavoiteMoviesDTO movies)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var user = dbContext.Users.Find(userId);

            if(user == null)
            {
                return NotFound();
            }

            user.FavoriteMovies = new List<Movie>(4);

            foreach (int movieId in movies.FavoriteMoviesIds)
            {
                var movie = dbContext.Movies.Find(movieId);
                
                if (movie != null)
                {
                    user.FavoriteMovies.Add(movie);
                }
            }

            dbContext.SaveChanges();

            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        [Route("{userId:int}")]
        public IActionResult FollowAnotherUser([FromRoute] int userId)
        {
            var connectedUserId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var connectedUser = dbContext.Users.Find(connectedUserId);
            var followingUser = dbContext.Users.Find(userId);

            if (followingUser == null)
            {
                return NotFound("Imposible de suivre un utilisateur qui n'existe pas");
            }

            connectedUser.Following.Add(followingUser);
            followingUser.Followers.Add(connectedUser);

            dbContext.SaveChanges();

            return Ok();
        }
    }
}