namespace MovieApp.Models
{
    public class UpdateMovieDto
    {
        public required string Title { get; set; }
        public string? Director { get; set; }
        public required int Year { get; set; }
        public float? Rate { get; set; }
    }
}
