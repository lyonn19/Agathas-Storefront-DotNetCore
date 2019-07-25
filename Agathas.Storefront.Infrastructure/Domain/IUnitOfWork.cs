using System;

using Microsoft.AspNetCore.Http;

namespace Agathas.Storefront.Infrastructure.Domain {
  public interface IUnitOfWork<TContext> : IDisposable {

    TContext Context { get;}
    void RegisterAmended(IAggregateRoot entity,
                          IUnitOfWorkRepository unitofWorkRepository);
    void RegisterNew(IAggregateRoot entity,
                      IUnitOfWorkRepository unitofWorkRepository);
    void RegisterRemoved(IAggregateRoot entity,
                          IUnitOfWorkRepository unitofWorkRepository);
    void Commit();
  }
}

