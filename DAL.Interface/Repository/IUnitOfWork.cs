using System;

namespace DAL.Interface.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Pattern UOW
        /// </summary>
        void Commit();
        //Rollback
    }
}