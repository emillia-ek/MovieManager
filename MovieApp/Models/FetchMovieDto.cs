namespace MovieApp.Models
{
    public class FetchMovieDto
    {
        public int Id { get; set; } // To jest ID z API (ExternalId w bazie)
        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
    }
}
