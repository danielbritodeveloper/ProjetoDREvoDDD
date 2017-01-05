using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ProjetoDREvoDDD.Domain.Interfaces.Repositories;
using ProjetoDREvoDDD.Domain.Interfaces.Services;

namespace ProjetoDREvoDDD.Domain.Services
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            this._repository = repository;
        }

        void IServiceBase<TEntity>.Add(TEntity item)
        {
            this._repository.Add(item);
        }

        IEnumerable<TEntity> IServiceBase<TEntity>.Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this._repository.Get(predicate);
        }

        IEnumerable<TEntity> IServiceBase<TEntity>.GetAll()
        {
            return this._repository.GetAll();
        }

        void IServiceBase<TEntity>.Remove(TEntity item)
        {
            this._repository.Remove(item);
        }

        void IServiceBase<TEntity>.Update(TEntity item)
        {
            this._repository.Update(item);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ServiceBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
