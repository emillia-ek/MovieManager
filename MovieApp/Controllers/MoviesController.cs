using System.Runtime.CompilerServices;
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
        private readonly MovieService movieService;
        public MoviesController(ApplicationDbContext dbContext, MovieService movieService)
        {
            this.dbContext = dbContext;
            this.movieService = movieService;
        }

        [HttpPost("fetch")]
        public async Task<IActionResult> FetchMoviesFromApi()
        {
            try
            {
                var newMovies = await movieService.DownloadAndSaveMovie();
                return Ok(newMovies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Błąd pobierania filmów: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            try
            {
                var movies = dbContext.Movies.ToList();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Błąd serwera", details = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetMovieById(int id)
        {
            try
            {
                var movie = dbContext.Movies.Find(id);
                if (movie is null)
                {
                    return NotFound(new { message = "Nie znaleziono filmu", id });
                }
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Błąd serwera", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult AddMovie(AddMovieDto addMovieDto)
        {
            try
            {
                if (addMovieDto == null)
                {
                    return BadRequest(new { message = "Nieprawidłowe dane wejściowe" });
                }

                var movieEntity = new Movie
                {
                    Title = addMovieDto.Title,
                    Director = addMovieDto.Director,
                    Year = addMovieDto.Year,
                    Rate = addMovieDto.Rate
                };

                dbContext.Movies.Add(movieEntity);
                dbContext.SaveChanges();

                return CreatedAtAction(nameof(GetMovieById), new { id = movieEntity.Id }, movieEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Błąd serwera", details = ex.Message });
            }
        }


        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateMovie(int id, UpdateMovieDto updateMovieDto)
        {
            try
            {
                var movie = dbContext.Movies.Find(id);
                if (movie == null)
                {
                    return NotFound(new { message = "Film nie znaleziony", id });
                }

                if (updateMovieDto == null)
                {
                    return BadRequest(new { message = "Nieprawidłowe dane wejściowe" });
                }

                movie.Title = updateMovieDto.Title;
                movie.Director = updateMovieDto.Director;
                movie.Year = updateMovieDto.Year;
                movie.Rate = updateMovieDto.Rate;

                dbContext.SaveChanges();
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Błąd serwera", details = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                var movie = dbContext.Movies.Find(id);
                if (movie == null)
                {
                    return NotFound(new { message = "Film nie znaleziony", id });
                }

                dbContext.Movies.Remove(movie);
                dbContext.SaveChanges();
                return Ok(new { message = "Film został usunięty" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Błąd serwera", details = ex.Message });
            }
        }
    }
}
