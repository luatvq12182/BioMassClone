using server.DataAccess.Persistence;
using server.DataAccess.Repositories;

namespace server.DataAccess.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _session;
        public ICategoryRepository Category {get;}

        public ICatLangsRepository CatLang {get;}

        public IPostRepository Post {get;}

        public IPostLangRepository PostLang {get;}

        public IImageRepository Image {get;}

        public IUserRepository User {get;}

        public ILanguageRepository Language {get;}

        public UnitOfWork(DbSession session, ICategoryRepository category, ICatLangsRepository catLang, IPostRepository post, IPostLangRepository postLang, IImageRepository image, IUserRepository user, ILanguageRepository language)
        {
            _session = session;
            Category = category;
            CatLang = catLang;
            Post = post;
            PostLang = postLang;
            Image = image;
            User = user;
            Language = language;
        }

        public async Task<bool> CommitThings()
        {
            try
            {

                //await _mediator.DispatchDomainEvents(this);

                _session.Transaction.Commit();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not commit the transaction, reason: {e.Message}");
                _session.Transaction.Rollback();
                return false;
            }
            finally
            {
                _session.Transaction.Dispose();
            }
        }
    }
}
