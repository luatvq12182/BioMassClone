using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace server.DataAccess.EF
{
    public class BioMassDbContextFactory : IDesignTimeDbContextFactory<BioMassDbContext>
    {
        public BioMassDbContext CreateDbContext(string[] args)
        {

            var optionBuilder = new DbContextOptionsBuilder<BioMassDbContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            optionBuilder.UseMySQL(configuration.GetConnectionString("MySqlConn"));

            return new BioMassDbContext(optionBuilder.Options);
        }
    }
}

