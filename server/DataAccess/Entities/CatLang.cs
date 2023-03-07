namespace server.DataAccess.Entities
{
    public class CatLang
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
