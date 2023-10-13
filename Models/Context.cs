using Microsoft.EntityFrameworkCore;
using MovieBackAPI.Models;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Movie> Movies { get; set; }

    public DbSet<Actor> Actors { get; set; }

    public DbSet<ActorMovie> ActorsMovies { get; set; }

    public DbSet<Director> Directors { get; set; }

}