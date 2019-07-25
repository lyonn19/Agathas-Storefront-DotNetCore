using System;

using Web = Microsoft.AspNetCore.Http;
using NHibernate;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Infrastructure.Logging;
using Agathas.Storefront.Repository.NHibernate.SessionStorage;

namespace Agathas.Storefront.Repository.NHibernate {
  public class NHUnitOfWork : IUnitOfWork<Web.IHttpContextAccessor> {
    public Web.IHttpContextAccessor Context { get; set; }
    public NHUnitOfWork(Web.IHttpContextAccessor context) {
      this.Context = context;
    }

    public void RegisterAmended(IAggregateRoot entity,
                                IUnitOfWorkRepository unitofWorkRepository) {
      SessionFactory.GetCurrentSession(this.Context).SaveOrUpdate(entity);
    }

    public void RegisterNew(IAggregateRoot entity,
                            IUnitOfWorkRepository unitofWorkRepository) {
      SessionFactory.GetCurrentSession(this.Context).Save(entity);
    }

    public void RegisterRemoved(IAggregateRoot entity,
                                IUnitOfWorkRepository unitofWorkRepository){
      SessionFactory.GetCurrentSession(this.Context).Delete(entity);
    }

    public void Commit() {
      using (ITransaction transaction =
              SessionFactory.GetCurrentSession(this.Context).BeginTransaction()) {
        try { transaction.Commit(); }
        catch (Exception ex) {
          LoggingFactory.GetLogger().Log(ex.Message);
          transaction.Rollback();
          throw;
        }
      }
    }

    public void Dispose(){

    }
  }
}
