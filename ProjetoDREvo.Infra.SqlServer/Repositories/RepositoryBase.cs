using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Infra_SqlServer.Contexto;
using ProjetoDREvoDDD.Domain.Interfaces.Repositories;

namespace ProjetoDREvo.Infra.SqlServer.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected InfraSqlContext Db = new InfraSqlContext();

        void IRepositoryBase<TEntity>.Add(TEntity item)
        {
            this.Db.Set<TEntity>().Add(item);
            Db.SaveChanges();
        }

        IEnumerable<TEntity> IRepositoryBase<TEntity>.Get(Expression<Func<TEntity, bool>> predicate)
        {
            var itens = Db.Set<TEntity>()
                     .Where(predicate)
                     .ToList();

            return itens;
        }

        IEnumerable<TEntity> IRepositoryBase<TEntity>.GetAll()
        {
            IEnumerable<TEntity> itens = Db.Set<TEntity>()
                                            .ToList();

            return itens;
        }

        void IRepositoryBase<TEntity>.Remove(TEntity item)
        {
            Db.Set<TEntity>().Remove(item);
            Db.SaveChanges();
        }

        void IRepositoryBase<TEntity>.Update(TEntity item)
        {
            Db.Entry(item).State = EntityState.Modified;
            Db.SaveChanges();
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
        // ~RepositoryBase() {
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
