using Microsoft.EntityFrameworkCore;
using MovieBackAPI.Models;
using System.Reflection.Metadata;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }

    public DbSet<Actor> Actors { get; set; }

    public DbSet<ActorMovie> ActorsMovies { get; set; }

    public DbSet<Director> Directors { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var user1 = new User { Id = 901, Name = "user1", Password = "string" };
        var user2 = new User { Id = 902, Name = "user2", Password = "string" };
        var user3 = new User { Id = 903, Name = "user3", Password = "string" };

        var director1 = new Director 
        {   
            Id = 901,
            Name = "Quentin Tarentino",
            Biography = "string",
            DateOfBirth = new DateTime(day:10, month:10, year:1963)
        };

        var director2 = new Director
        {
            Id = 902,
            Name = "Christopher Nolan",
            Biography = "string",
            DateOfBirth = new DateTime(day: 10, month: 10, year: 1970)
        };

        var movie1 = new Movie
        {
            Id = 901,
            Title = "Oppenheimer",
            DirectorId = 2,
            Description = "string",
            Rating = 5,
            RealeaseYear = 2023
        };

        var movie2 = new Movie
        {
            Id = 902,
            Title = "Kill Bill",
            DirectorId = 1,
            Description = "string",
            Rating = 5,
            RealeaseYear = 2003
        }; 
        
        var movie3 = new Movie
        {
            Id = 903,
            Title = "Memento",
            DirectorId = 2,
            Description = "string",
            Rating = 10,
            RealeaseYear = 2000
        };

        var actor1 = new Actor
        {
            Id = 905,
            Name = "Uma Thurman",
            Biography = "string",
            DateOfBirth = new DateTime(day: 10, month: 10, year: 1970)
        };

        var actor2 = new Actor
        {
            Id = 906,
            Name = "Guy Pearce",
            Biography = "string",
            DateOfBirth = new DateTime(day: 10, month: 10, year: 1967)
        };

        var actorMovie1 = new ActorMovie
        {
            ActorId = 905,
            MovieId = 902,
        };
        var actorMovie2 = new ActorMovie
        {
            ActorId = 906,
            MovieId = 903,
        };
        var actorMovie3 = new ActorMovie
        {
            ActorId = 902,
            MovieId = 901,
        };
        var actorMovie4 = new ActorMovie
        {
            ActorId = 906,
            MovieId = 901,
        };

        modelBuilder.Entity<User>().HasData(user1,user2,user3);
        modelBuilder.Entity<Director>().HasData(director1, director2);
        modelBuilder.Entity<Actor>().HasData(actor1, actor2);
        modelBuilder.Entity<Movie>().HasData(movie1, movie2, movie3);
        modelBuilder.Entity<ActorMovie>().HasData(actorMovie1, actorMovie2, actorMovie3, actorMovie4);

        base.OnModelCreating(modelBuilder);
    }

}