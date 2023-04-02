using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;

namespace server.Services
{
    public interface IImageService : IGenericService<Image>
    {
        Task<bool> AddToSlider(List<int> ids);
        Task<bool> RemoveFromSlider(int id);
        Task<IReadOnlyList<string>> AlreadyInUse(string imageUrl);
    }
    public class ImageService : IImageService
    {

        private readonly IUnitOfWork _unit;
        public ImageService(IUnitOfWork unitOfWork)
        {
            _unit = unitOfWork;
        }

        public async Task<Image> AddAsync(Image entity)
        {
            return await _unit.Image.AddAsync(entity);
        }

        public async Task<bool> AddToSlider(List<int> ids)
        {
            bool isSuccessful = true;
            foreach (var imageId in ids)
            {
                isSuccessful &= await _unit.Image.AddToSlider(imageId);
            }
            if (isSuccessful && await _unit.CommitThings())
                return isSuccessful;
            return false;

        }

        public async Task<IReadOnlyList<string>> AlreadyInUse(string imageUrl)
        {
            return await _unit.Image.AlreadyInUse(imageUrl);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _unit.Image.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<Image>> GetAllAsync()
        {
            return await _unit.Image.GetAllAsync();
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            return await _unit.Image.GetByIdAsync(id);
        }

        public async Task<bool> RemoveFromSlider(int id)
        {
            return await _unit.Image.RemoveFromSlider(id);
        }

        public Task<Image> UpdateAsync(Image entity)
        {
            throw new NotImplementedException();
        }
    }
}
