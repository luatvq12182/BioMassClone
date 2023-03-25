using server.DataAccess.Entities;
using server.ViewModel.Categories;

namespace server.Helper.Mapper
{
    public static class CatLangMapper
    {
        public static CatLang MapToCatLangEntity (this CategoryModel model)
        {
            return new CatLang
            {
                Id= model.Id,
                LanguageId = model.LanguageId.Value,
                Name= model.Name,
                Slug= model.Slug
            };
        }
        public static CategoryModel MapToModel(this CatLang entity)
        {
            return new CategoryModel
            {
                Id= entity.Id,
                LanguageId = entity.LanguageId,
                Name = entity.Name,
                Slug = entity.Slug
            };
        }
        public static List<CatLang> MapToCatLangEntities (this List<CategoryModel> models) 
        {
            return models.Select(x => x.MapToCatLangEntity()).ToList();
        }
        public static List<CategoryModel> MapToModels(this List<CatLang> entities)
        {
            return entities.Select(x => x.MapToModel()).ToList();
        }

    }
}
