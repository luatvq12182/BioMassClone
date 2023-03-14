using server.DataAccess.Repositories;

namespace server.DataAccess.Common
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; } 
        ICatLangsRepository CatLang { get; }
        IPostRepository Post { get; }
        IPostLangRepository PostLang { get; }
        IImageRepository Image { get; }
        IUserRepository User { get; }
        ILanguageRepository Language { get; }
        Task<bool> CommitThings();
    }
}
