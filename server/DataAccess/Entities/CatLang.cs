﻿namespace server.DataAccess.Entities
{
    public class CatLang
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
    }
}
