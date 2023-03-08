using server.DataAccess.Common;
using server.DataAccess.EF;
using server.DataAccess.Entities;

namespace server.DataAccess.Repositories
{
    public interface IImageRepository : IBaseRepository<Image>
    {
    }
    public class ImageRepository : BaseRepository<Image> , IImageRepository
    {
        public ImageRepository(GreenWayDbContext context) :base(context) 
        {

        }
    }
}
