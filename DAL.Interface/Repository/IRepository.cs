using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Method for getting all entities
        /// </summary>
        /// <returns>All entities</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Method for finding entity by Id
        /// </summary>
        /// <param name="key">id</param>
        /// <returns>Entity</returns>
        TEntity GetById(int key);

        /// <summary>
        /// Method for finding entity by predicate
        /// </summary>
        /// <param name="f">Expression</param>
        /// <returns>Entity</returns>
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> f); // maybe, it doesn't work because it returns TEntity, not IEnumerable

        /// <summary>
        /// Creates entity in DB
        /// </summary>
        /// <param name="e">entity</param>
        void Create(TEntity e);

        /// <summary>
        /// Deletes entity from DB
        /// </summary>
        /// <param name="e">entity</param>
        void Delete(TEntity e);

        /// <summary>
        /// Updates entity in DB
        /// </summary>
        /// <param name="entity">entity</param>
        void Update(TEntity entity);
    }
}