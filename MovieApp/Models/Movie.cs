namespace MovieApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Director { get; set; }
        public int Year { get; set; }
    }
}
