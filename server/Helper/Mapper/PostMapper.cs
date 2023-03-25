using server.DataAccess.Entities;
using server.ViewModel.Posts;

namespace server.Helper.Mapper
{
    public static class PostMapper
    {
        public static Post MapToEntity(this PostModel model)
        {
            return new Post
            {
                Id = model.Id,
                CategoryId= model.CategoryId.Value,
                Title = model.Title,
                Body= model.Body,
                Thumbnail = model.Thumbnail,
                ShortDescription= model.ShortDescription,   
                Views= model.Views,
                Author= model.Author,
                CreatedDate= model.CreatedDate
            };
        }
        public static PostModel MapToModel(this Post entity)
        {
            return new PostModel
            {
                Id = entity.Id,
                CategoryId = entity.CategoryId,
                Title = entity.Title,
                Body = entity.Body,
                Thumbnail = entity.Thumbnail,
                ShortDescription = entity.ShortDescription,
                Views = entity.Views,
                Author = entity.Author,
                CreatedDate = entity.CreatedDate,
                LanguageId = null
            };
        }
        public static List<Post> MapToCatLangEntities(this List<PostModel> models)
        {
            return models.Select(x => x.MapToEntity()).ToList();
        }
        public static List<PostModel> MapToModels(this List<Post> entities)
        {
            return entities.Select(x => x.MapToModel()).ToList();
        }
    }
}
