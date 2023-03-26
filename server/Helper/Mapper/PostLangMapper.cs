using server.DataAccess.Entities;
using server.ViewModel.Posts;
namespace server.Helper.Mapper
{
    public static class PostLangMapper
    {
        public static PostLang MapToPostLangEntity (this PostModel model)
        {
            return new PostLang
            {
                Id = model.Id,
                Title= model.Title,
                ShortDescription= model.ShortDescription,
                Body= model.Body,
                LangId = model.LanguageId.Value,
                PostId = model.PostId
            };
        }
    }
}
