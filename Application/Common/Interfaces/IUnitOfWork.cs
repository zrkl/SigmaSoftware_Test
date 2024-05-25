using Domain.Common;
using System.Data;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IUnitOfWork : IDisposable 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepositoryAsync<T> Repository<T>() where T : BaseEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> Commit(CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task Rollback();

        /// <summary>
        /// Method to execute SQL query
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        Task<int> Delete(BaseEntity entity, CancellationToken cancellationToken);
    }
}