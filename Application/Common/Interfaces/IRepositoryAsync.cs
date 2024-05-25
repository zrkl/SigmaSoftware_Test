using Domain.Common;
using Domain;
using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// Generic class interface to handle the database interaction
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IRepositoryAsync<T> where T : class
    {
        /// <summary>
        /// Entity to manage
        /// </summary>
        IQueryable<T> Entities { get; }

        /// <summary>
        /// Method to get entity details based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Method to get all the entity records
        /// </summary>
        /// <param name="includes"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<T>> GetAllAsync(string[] includes = null, int? take = null);

        /// <summary>
        /// Method to search on a table, based on criterias
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<T> Find(Expression<Func<T, bool>> criteria, string[] includes = null);

        /// <summary>
        /// Method to handle the pagination
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Method to add a record to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);
        
        /// <summary>
        /// Method to add a collection of entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddRangeAsync(List<T> entities);

        /// <summary>
        /// Method to update a record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Method to delete a record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);
    }
}