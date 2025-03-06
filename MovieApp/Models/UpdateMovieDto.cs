namespace MovieApp.Models
{
    public class UpdateMovieDto
    {
        public required string Title { get; set; }
        public required string Director { get; set; }
        public required int Year { get; set; }
    }
}
