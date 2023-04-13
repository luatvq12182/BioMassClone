namespace server.ViewModel.Categories
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int? LanguageId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsStaticCategory { get; set; }
        public bool ShowOnHeaderMenu { get; set; }
    }
}
