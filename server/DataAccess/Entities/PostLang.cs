namespace server.DataAccess.Entities
{
    public class PostLang
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int LangId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ShortDescription { get; set; }
    }
}
