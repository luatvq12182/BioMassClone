using log4net;
using server.DataAccess.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace server.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}
