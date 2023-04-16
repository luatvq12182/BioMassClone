﻿namespace server.DataAccess.Entities
{
    public class PostLang
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ShortDescription { get; set; }
        public string Slug { get; set; }
    }
}
