using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;
using server.ViewModel.Categories;

namespace server.Services
{
    public interface ICategoryService : IEntityService<Category>
    {
        Task<CategoryModel> InsertCategoryTransactional(CategoryModel model);
    }
    public class CategoryService : EntityService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICatLangsRepository _catLangRepository;
        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository , ICatLangsRepository catLangsRepository) : base(unitOfWork, categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _catLangRepository = catLangsRepository;
           
        }

        public async Task<CategoryModel> InsertCategoryTransactional(CategoryModel model)
        {
            var category =  await _categoryRepository.Insert(new Category
            {
                Name = model.Name,
                Slug = model.Slug
            });

            var catLang = new CatLang
            {
                CategoryId = category.Id,
                LanguageId= model.LanguageId!.Value,
                Name= model.Name,
                Slug = model.Slug
            };
            await _catLangRepository.Insert(catLang);

            UnitOfWork.TransactionSaveChanges();

            return new CategoryModel
            {
                Id= category.Id,
                Name= model.Name,
                Slug = model.Slug
            };
        }
    }
}
