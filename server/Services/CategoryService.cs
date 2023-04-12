using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.Helper.Mapper;
using server.ViewModel.Categories;
using static Dapper.SqlMapper;


namespace server.Services
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<IReadOnlyList<CategoryModel>> InsertCategoryTransactional(IReadOnlyList<CategoryModel> model);
        Task<IReadOnlyList<CategoryModel>> GetDetails(int id);
        Task<bool> Remove(int id);
        Task<bool> Edit(int id, IReadOnlyList<CategoryModel> model);
        Task<IReadOnlyList<CategoryModel>> GetByLanguageId(int? languageId);
        Task<IReadOnlyList<CategoryModel>> UpdateTransactionalAsync(IReadOnlyList<CategoryModel> model);
    }
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unit;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unit= unitOfWork;
           
        }

        public Task<Category> AddAsync(Category entity)
        {
            return _unit.Category.AddAsync  (entity);
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(int id, IReadOnlyList<CategoryModel> model)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            return await _unit.Category.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _unit.Category.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<CategoryModel>> GetByLanguageId(int? languageId)
        {
            if (!languageId.HasValue)
            {
                var categories =  await _unit.Category.GetAll();
                if(categories != null && categories.Any())
                {
                    return categories;
                }
                return null;
            }
            else
            {
                return await _unit.CatLang.GetByLanguageId(languageId.Value);
            }

        }

        public async Task<IReadOnlyList<CategoryModel>> GetDetails(int id)
        {
            var result = new List<CategoryModel>(); 
            var standardItem = await _unit.Category.GetByIdAsync(id);
            if (standardItem != null)
            {
                result.Add(standardItem.MapToModel());

                var specificItems = await _unit.CatLang.GetByCategoryId(id);
                result.AddRange(specificItems);
                return result;
            }
            return null;
        }

        public async Task<IReadOnlyList<CategoryModel>> InsertCategoryTransactional(IReadOnlyList<CategoryModel> model)
        {
            var standardItem = model.FirstOrDefault(x => x.LanguageId is null);
            if(standardItem != null)
            {
                var insertedCategory =   await _unit.Category.AddTransactionalAsync(new Category {Name = standardItem.Name, Slug = standardItem.Slug });
                var spescificItems = model.Where(x => x.LanguageId > 0).ToList();
                if(spescificItems != null && spescificItems.Any())
                {
                    foreach (var item in spescificItems)
                    {
                        await _unit.CatLang.AddTransactionalAsync(new CatLang {CategoryId = insertedCategory.Id,  LanguageId = item.LanguageId.Value, Name = item.Name, Slug = item.Slug });
                    }                  
                }
                if( await _unit.CommitThings())
                {
                    return model;
                }
            }
            return null;

        }

        public async Task<bool> Remove(int id)
        {
            var entity = await _unit.Category.GetByIdAsync(id);
            if(entity!= null)
            {
                var specificItems = await _unit.CatLang.GetByCategoryId(id);
                if (specificItems != null && specificItems.Any())
                {
                    foreach (var item in specificItems)
                    {
                        await _unit.CatLang.DeleteAsync(item.Id);
                    }
                }
                await _unit.Category.DeleteAsync(entity.Id);
                return true;
            }
            return false;

        }

        public Task<Category> UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<CategoryModel>> UpdateTransactionalAsync(IReadOnlyList<CategoryModel> model)
        {
            var standardItem = model.FirstOrDefault(x => x.LanguageId is null);
            await _unit.Category.UpdateTransactionalAsync(standardItem.MapToCategoryEntity());
            var specificItems = model.Where(x => x.LanguageId.HasValue && x.LanguageId.Value > 0).ToList();
            
            if (specificItems != null && specificItems.Any())
            {
                foreach (var catLang in specificItems)
                {
                    if(catLang.Id == 0)
                    {
                        await _unit.CatLang.AddTransactionalAsync(catLang.MapToCatLangEntity());
                    }
                    else
                        await _unit.CatLang.UpdateTransactionalAsync(catLang.MapToCatLangEntity());
                }
            }
            if (await _unit.CommitThings())
            {
                return model;
            }
            else
                return null;
        }
    }
}
