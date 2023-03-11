using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;
using server.ViewModel.Categories;
using System.Collections.Generic;

namespace server.Services
{
    public interface ICategoryService : IEntityService<Category>
    {
        Task<IReadOnlyList<CategoryModel>> InsertCategoryTransactional(IReadOnlyList<CategoryModel> model);
        Task<IReadOnlyList<CategoryModel>> GetDetails(int id);
        Task<bool> Remove(int id);
        Task<bool> Edit(int id, IReadOnlyList<CategoryModel> model);
        Task<IReadOnlyList<CategoryModel>> GetByLanguageId(int? languageId);
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

        public async Task<bool> Edit(int id, IReadOnlyList<CategoryModel> model)
        {
            var standardItem = _categoryRepository.Find(x => x.Id == id);
            if (standardItem != null)
            {
                if(model != null && model.Any())
                {
                    foreach (var item in model)
                    {
                        if(item.LanguageId is null)
                        {
                            standardItem.Name = item.Name;
                            standardItem.Slug= item.Slug;
                            await _categoryRepository.Update(standardItem);
                            UnitOfWork.SaveChanges();
                        }
                        else
                        {
                            var entity = await _catLangRepository.GetById(item.Id);
                            if(entity != null)
                            {
                                entity.Name = item.Name;
                                entity.Slug = item.Slug;
                                await _catLangRepository.Update(entity);
                                UnitOfWork.SaveChanges();
                            }
                            else 
                                return false;
                        }
                    }
                    return true;
                }

            }
            return false;
        }

        public async Task<IReadOnlyList<CategoryModel>> GetByLanguageId(int? languageId)
        {
            var result = new List<CategoryModel>();
            if (languageId.HasValue)
            {
                var data =  _catLangRepository.FindAll(x=>x.LanguageId == languageId.Value).ToList();
                if(data != null && data.Any())
                {
                    foreach (var item in data)
                    {
                        result.Add(new CategoryModel
                        {
                            Id = item.Id,
                            LanguageId = item.LanguageId,
                            Name = item.Name,
                            Slug = item.Slug
                        });
                    }
                }
            }
            else
            {
                var data = await _categoryRepository.GetAll();
                if(data != null && data.Any())
                {
                    foreach (var item in data)
                    {
                        result.Add(new CategoryModel
                        {
                            Id = item.Id,
                            LanguageId = null,
                            Name = item.Name,
                            Slug = item.Slug
                        });
                    }
                }
            }
            return result;
        }

        public async Task<IReadOnlyList<CategoryModel>> GetDetails(int id)
        {
            var result = new List<CategoryModel>();
            var standardItem = await _categoryRepository.GetById(id);
            if(standardItem != null)
            {
                result.Add(new CategoryModel
                {
                    Id = standardItem.Id,
                    LanguageId = null,
                    Name = standardItem.Name,
                    Slug = standardItem.Slug
                });
                var specificItems = _catLangRepository.FindAll(x => x.CategoryId == id).ToList();

                if (specificItems != null && specificItems.Any())
                {
                    foreach (var item in specificItems)
                    {
                        result.Add(new CategoryModel
                        {
                            Id = item.Id,
                            LanguageId = item.LanguageId,
                            Name = item.Name,
                            Slug = item.Slug
                        });
                    }
                }
                return result;
            }
            return null;
        }

        public async Task<IReadOnlyList<CategoryModel>> InsertCategoryTransactional(IReadOnlyList<CategoryModel> model)
        {
            if (model != null && model.Any()) 
            {
                var standardId = 0;
                foreach (var item in model)
                {
                    if(item.LanguageId is null)
                    {
                        var  insertedItem = await _categoryRepository.Insert(new Category
                        {
                            Slug= item.Slug,
                            Name = item.Name

                        });

                        UnitOfWork.SaveChanges();
                        standardId = insertedItem.Id;
                    }
                    else
                    {
                        await _catLangRepository.Insert(new CatLang
                        {
                            CategoryId = standardId,
                            LanguageId = item.LanguageId.Value,
                            Name = item.Name,
                            Slug = item.Slug,
                        });
                    }
                }
            }
            UnitOfWork.SaveChanges();
            return model;
        }

        public async Task<bool> Remove(int id)
        {
            var standardItem = _categoryRepository.Find(x => x.Id == id);
            if (standardItem != null)
            {
                var specificItems = _catLangRepository.FindAll(x=>x.CategoryId == id);
                if (specificItems != null && specificItems.Any()) 
                {
                    foreach (var item in specificItems) 
                    {
                        _catLangRepository.Delete(item);
                        UnitOfWork.SaveChanges();
                    }
                }
                _categoryRepository.Delete(standardItem);
                UnitOfWork.SaveChanges(); 
                return true;
            }
            return false;
        }
    }
}
