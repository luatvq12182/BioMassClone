using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;

namespace server.Services
{
    public interface IImageService : IEntityService<Image>
    {

    }
    public class ImageService : EntityService<Image>, IImageService
    {
        private readonly IImageRepository _repos;
        public ImageService(IUnitOfWork unitOfWork , IImageRepository repository) : base (unitOfWork,repository)
        {
            _repos = repository;
        }
    }
}
