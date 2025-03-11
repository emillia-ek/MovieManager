namespace MovieApp.Models
{
    public class FetchMovieDto
    {
        public int Id { get; set; } // To jest ID z API (ExternalId w bazie)
        public required string Title { get; set; }
        public required string Director { get; set; }
        public required int Year { get; set; }
        public required float Rate { get; set; }
    }
}
