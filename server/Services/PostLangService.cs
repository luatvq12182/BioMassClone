using server.DataAccess.Common;
using server.DataAccess.Entities;
using server.DataAccess.Repositories;

namespace server.Services
{
    public interface IPostLangService : IEntityService<PostLang>
    {

    }
    public class PostLangService : EntityService<PostLang>, IPostLangService
    {
        private readonly IPostLangRepository _repos;
        public PostLangService(IUnitOfWork unitOfWork , IPostLangRepository repository) : base(unitOfWork,repository)
        {
            _repos= repository;
        }
    }
}
