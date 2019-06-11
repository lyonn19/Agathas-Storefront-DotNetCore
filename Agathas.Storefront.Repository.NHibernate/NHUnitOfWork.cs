using System;

using Web = Microsoft.AspNetCore.Http;
using NHibernate;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Infrastructure.Logging;
using Agathas.Storefront.Repository.NHibernate.SessionStorage;

namespace Agathas.Storefront.Repository.NHibernate {
  public class NHUnitOfWork : IUnitOfWork {
    private readonly Web.IHttpContextAccessor _context;
    public NHUnitOfWork(Web.IHttpContextAccessor context) {
      this._context = context;
    }

    public void RegisterAmended(IAggregateRoot entity,
                                IUnitOfWorkRepository unitofWorkRepository) {
      SessionFactory.GetCurrentSession(this._context).SaveOrUpdate(entity);
    }

    public void RegisterNew(IAggregateRoot entity,
                            IUnitOfWorkRepository unitofWorkRepository) {
      SessionFactory.GetCurrentSession(this._context).Save(entity);
    }

    public void RegisterRemoved(IAggregateRoot entity,
                                IUnitOfWorkRepository unitofWorkRepository){
      SessionFactory.GetCurrentSession(this._context).Delete(entity);
    }

    public void Commit() {
      using (ITransaction transaction =
              SessionFactory.GetCurrentSession(this._context).BeginTransaction()) {
        try { transaction.Commit(); }
        catch (Exception ex) {
          LoggingFactory.GetLogger().Log(ex.Message);
          transaction.Rollback();
          throw;
        }
      }
    }
  }
}
