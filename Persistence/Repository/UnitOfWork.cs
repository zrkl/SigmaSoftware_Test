using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using System.Collections;
using System.Data;

namespace Persistence.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Database context
        /// </summary>
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Boolean indicating if the connexion is disposed
        /// </summary>
        private bool disposed;

        /// <summary>
        /// TBD
        /// </summary>
        private Hashtable _repositories;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IRepositoryAsync<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryAsync<TEntity>);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepositoryAsync<TEntity>)_repositories[type];
        }

        /// <summary>
        /// Method to commit the database changes
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> Commit(CancellationToken cancellationToken)
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries<BaseEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        entry.Entity.CreatedBy = "";
                        entry.Entity.LastModifiedOn = DateTime.Now;
                        entry.Entity.LastModifiedBy = "";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.Now;
                        entry.Entity.LastModifiedBy = "";
                        break;

                    case EntityState.Deleted:
                        entry.Entity.DeletedOn = DateTime.Now;
                        entry.Entity.DeletedBy = "";
                        entry.Entity.IsDeleted = true;
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }


        /// <summary>
        /// Method to commit the database changes
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> Delete(BaseEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Remove(entity);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }


        /// <summary>
        /// Method to rollback the changes
        /// </summary>
        /// <returns></returns>
        public Task Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }

        /// <summary>
        /// Method to clean Garbage Collector
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Method to clean Garbage Collector
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }

            //dispose unmanaged resources
            disposed = true;
        }
    }
}