namespace server.DataAccess.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? Slug { get; set; }
        public string? Name { get; set; }
        public bool IsStaticCategory { get; set; }
        public bool ShowOnHeaderMenu { get; set; }
    }
}
