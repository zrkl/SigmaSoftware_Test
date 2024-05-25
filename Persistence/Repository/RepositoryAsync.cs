using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using System.Linq.Expressions;

namespace Persistence.Repository
{
    /// <summary>
    /// Generic class to handle the database interaction
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : BaseEntity
    {
        /// <summary>
        /// Database context
        /// </summary>
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Entity to manage
        /// </summary>
        public IQueryable<T> Entities => _dbContext.Set<T>();

        /// <summary>
        /// Method to add a record to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        /// <summary>
        /// Method to add a collection of entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        /// <summary>
        /// Method to delete a record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Method to get all the entity records
        /// </summary>
        /// <param name="includes"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync(string[] includes = null, int? take = null)
        {
            //We get all the records
            IQueryable<T> query = _dbContext.Set<T>().Where(p => p.DeletedOn == null);

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (take != null)
                query = query.Take((int)take);

            //We return the result
            return await query.ToListAsync();
        }

        /// <summary>
        /// Method to search on a table, based on criterias
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<T> Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(p => p.DeletedOn == null);

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.FirstOrDefaultAsync(criteria);
        }

        /// <summary>
        /// TBD
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<List<T>> FindList(string[] includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(p => p.DeletedOn == null);

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Method to get entity details based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().Where(p => p.DeletedOn == null && p.Id.Equals(id)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method to handle the pagination
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Where(p => p.DeletedOn == null)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Method to update a record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task UpdateAsync(T entity)
        {
            T exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

    }
}