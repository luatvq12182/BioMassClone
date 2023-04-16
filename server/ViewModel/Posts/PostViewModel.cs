namespace server.ViewModel.Posts
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Views { get; set; }
        public string? Author { get; set; }
        public bool IsShowOnHomePage { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ShortDescription { get; set; }
        public string Slug { get; set; }
    }
}
