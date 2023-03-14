using server.DataAccess.Common;
using server.DataAccess.Persistence;
using server.DataAccess.Repositories;

namespace server.Services
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbSession>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICatLangsRepository, CatLangRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostLangRepository, PostLangRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

        }
    }
}
