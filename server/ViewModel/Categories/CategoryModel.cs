namespace server.ViewModel.Categories
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public int? CatLangId {get;set;}
        public int? LanguageId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
