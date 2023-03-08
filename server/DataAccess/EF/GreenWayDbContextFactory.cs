using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace server.DataAccess.EF
{
    public class GreenWayDbContextFactory : IDesignTimeDbContextFactory<GreenWayDbContext>
    {
        public GreenWayDbContext CreateDbContext(string[] args)
        {

            var optionBuilder = new DbContextOptionsBuilder<GreenWayDbContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            optionBuilder.UseMySQL(configuration.GetConnectionString("MySqlConn"));

            return new GreenWayDbContext(optionBuilder.Options);
        }
    }
}

