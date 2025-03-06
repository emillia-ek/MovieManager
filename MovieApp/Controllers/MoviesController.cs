using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Data;
using MovieApp.Models;
using MovieApp.Models.Entities;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public MoviesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllMovies()
        {
            return Ok(dbContext.Movies.ToList());
        }

        [HttpPost]
        public IActionResult AddMovie(AddMovieDto addMovieDto)
        {
            var movieEntity = new Movie()
            {
                Title = addMovieDto.Title,
                Director = addMovieDto.Director,
                Year = addMovieDto.Year
            };

            dbContext.Movies.Add(movieEntity);
            dbContext.SaveChanges();

            return Ok(movieEntity);

        }
    }
}
