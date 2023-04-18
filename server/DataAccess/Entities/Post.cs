namespace server.DataAccess.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Views { get; set; }
        public string? Author { get; set; }
        public bool ShowOnHomePage { get; set; }     
    }
}
