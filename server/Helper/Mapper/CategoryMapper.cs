using server.DataAccess.Entities;
using server.ViewModel.Categories;

namespace server.Helper.Mapper
{
    public static class CategoryMapper 
    {
        public static Category MapToCategoryEntity(this CategoryModel model)
        {
            return new Category
            {
                Id = model.Id,
                Name = model.Name,
                Slug = model.Slug,
                IsStaticCategory= model.IsStaticCategory,
                ShowOnHeaderMenu= model.ShowOnHeaderMenu,
            };
        }
        public static CategoryModel MapToModel(this Category entity)
        {
            return new CategoryModel
            {
                Id= entity.Id,
                Name = entity.Name,
                Slug = entity.Slug,
                IsStaticCategory= entity.IsStaticCategory,
                ShowOnHeaderMenu= entity.ShowOnHeaderMenu,
            };
        }
        public static List<Category> MapToCatLangEntities(this List<CategoryModel> models)
        {
            return models.Select(x => x.MapToCategoryEntity()).ToList();
        }
        public static List<CategoryModel> MapToModels(this List<Category> entities)
        {
            return entities.Select(x => x.MapToModel()).ToList();
        }
    }
}
