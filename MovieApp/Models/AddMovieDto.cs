namespace MovieApp.Models
{
    public class AddMovieDto
    {
        public required string Title { get; set; }
        public required string Director { get; set; }
        public required int Year { get; set; }
    }
}
