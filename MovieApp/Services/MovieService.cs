using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models.Entities;

public class MovieService
{
    private readonly HttpClient httpClient;
    private readonly ApplicationDbContext dbContext;

    public MovieService(HttpClient httpClient, ApplicationDbContext context)
    {
        this.httpClient = httpClient;
        this.dbContext = context;
    }

    public async Task<List<Movie>> PobierzFilmyIZapisz()
    {
        var response = await httpClient.GetAsync("https://filmy.programdemo.pl/MyMovies");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var moviesFromApi = JsonSerializer.Deserialize<List<FetchMovieDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (moviesFromApi == null) return new List<Movie>();

        var newMovies = new List<Movie>();

        foreach (var movieDto in moviesFromApi)
        {
            // ORM - Sprawdzamy, czy film już istnieje w bazie na podstawie ExternalId
            var existingMovie = await dbContext.Movies.FirstOrDefaultAsync(m => m.ExternalId == movieDto.Id);
            if (existingMovie == null)
            {
                var newMovie = new Movie
                {
                    ExternalId = movieDto.Id,
                    Title = movieDto.Title,
                    Director = movieDto.Director,
                    Year = movieDto.Year
                };

                newMovies.Add(newMovie);
            }
        }

        if (newMovies.Any())
        {
            dbContext.Movies.AddRange(newMovies);
            await dbContext.SaveChangesAsync(); 
        }

        return newMovies;
    }
}

