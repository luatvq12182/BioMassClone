using server.DataAccess.Entities;

namespace server.ViewModel.Posts
{
    public class PostModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Views { get; set; }
        public string? Author { get; set; }
        public bool IsShowOnHomePage { get; set; }
        public List<PostLang> Items { get; set; }
    }
}
