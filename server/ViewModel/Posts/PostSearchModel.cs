using server.ViewModel.Commons;

namespace server.ViewModel.Posts
{
    public class PostSearchModel : Pagination
    {
        public string? Lang { get; set; }
        public int? CategoryId { get; set; }
    }
}
