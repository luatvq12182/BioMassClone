using Microsoft.EntityFrameworkCore;
using server.DataAccess.Entities;

namespace server.DataAccess.EF
{
    public class BioMassDbContext : DbContext
    {
        public BioMassDbContext(DbContextOptions opt) : base(opt) 
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<CatLang> CatLangs { get; set; }
        public DbSet<PostLang> PostLangs { get; set; }      
    }
}
