﻿using log4net;
using server.DataAccess.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace server.Services
{
    public interface IEntityService<T> where T : class
    {
        /// <summary>
        /// Function use to get Object flow Id
        /// </summary>
        /// <param name="id">Primary key of Table current</param>
        /// <returns></returns>
        Task<T> GetById(int id);

        T GetById(long id);

        /// <summary>
        /// Get All list Object
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAll();

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
        bool Delete(dynamic id);

        /// <summary>
        /// Deletes the mullti.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool DeleteMulti(List<T> entity);

        T Find(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        List<T> FindAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

    }
    public class EntityService<T> : IEntityService<T> where T : class
    {
        protected readonly IUnitOfWork UnitOfWork;
        private readonly IBaseRepository<T> _repository;
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public EntityService(IUnitOfWork unitOfWork, IBaseRepository<T> repository)
        {
            UnitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<T> Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            await _repository.Insert(entity);
            UnitOfWork.SaveChanges();
            return entity;
        }

        public List<T> InsertMulti(List<T> entity)
        {
            try
            {
                _repository.InsertMulti(entity);
                UnitOfWork.SaveChanges();

                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                _repository.Delete(entity);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(dynamic id)
        {
            return _repository.Delete(id);
        }

        public bool DeleteMulti(List<T> entity)
        {
            try
            {
                _repository.DeleteMulti(entity);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public T GetById(long id)
        {
            return _repository.GetById(id);
        }

        public T Find(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return _repository.Find(expression, includes);
        }

        public List<T> FindAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return _repository.FindAll(expression, includes);
        }

        public async Task<List<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<T> Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            await _repository.Update(entity);
            UnitOfWork.SaveChanges();

            return entity;
        }
    }
}
