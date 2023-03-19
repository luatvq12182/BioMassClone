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

        public Task<IReadOnlyList<Image>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Image> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Image> UpdateAsync(Image entity)
        {
            throw new NotImplementedException();
        }
    }
}
