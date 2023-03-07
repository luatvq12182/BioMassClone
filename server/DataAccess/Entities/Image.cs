namespace server.DataAccess.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string ImageUrl { get; set; }
        public Post Post { get; set; }
    }
}
