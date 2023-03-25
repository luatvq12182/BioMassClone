using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;

namespace server.Services
{
    public interface IImageService : IGenericService<Image>
    {

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

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Image>> GetAllAsync()
        {
            return await _unit.Image.GetAllAsync();
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            return await _unit.Image.GetByIdAsync(id);
        }

        public Task<Image> UpdateAsync(Image entity)
        {
            throw new NotImplementedException();
        }
    }
}
