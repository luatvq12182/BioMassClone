﻿using System.Linq.Expressions;

namespace server.DataAccess.Common
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Function use to get Object flow Id
        /// </summary>
        /// <param name="id">Primary key of Table current</param>
        /// <returns></returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Function use to get Object flow Id
        /// </summary>
        /// <param name="id">Primary key of Table current</param>
        /// <returns></returns>
        T GetById(long id);

        /// <summary>
        /// Get All list Object
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// Function use in the case Query have condition
        /// </summary>
        /// <param name="filter">Condition of query</param>
        /// <returns></returns>
        IQueryable<T> Query(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Function use to Update Object
        /// </summary>
        /// <param name="entity">Object is targer Update</param>
        /// <returns></returns>
        Task<T> Update(T entity);

        /// <summary>
        /// Function use to Insert Object
        /// </summary>
        /// <param name="entity">Object is targer Update</param>
        /// <returns></returns>
        Task<T> Insert(T entity);

        /// <summary>
        /// Inserts the multiple entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        List<T> InsertMulti(List<T> entity);

        /// <summary>
        /// Function use to Remove Object in Database
        /// </summary>
        /// <param name="entity">Object is targer Update</param>
        /// <returns></returns>
        bool Delete(T entity);

        /// <summary>
        /// Function use to Remove Object in Database
        /// </summary>
        /// <param name="id">Id is identity</param>
        /// <returns></returns>
        Task<bool> Delete(int id);

        /// <summary>
        /// Deletes the mullti.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool DeleteMulti(List<T> entity);

        T Find(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        List<T> FindAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
    }
}
