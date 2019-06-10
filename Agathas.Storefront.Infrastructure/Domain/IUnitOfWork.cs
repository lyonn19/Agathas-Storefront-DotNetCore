namespace Agathas.Storefront.Infrastructure.Domain {
  public interface IUnitOfWork{
    void RegisterAmended(IAggregateRoot entity,
                          IUnitOfWorkRepository unitofWorkRepository);
    void RegisterNew(IAggregateRoot entity,
                      IUnitOfWorkRepository unitofWorkRepository);
    void RegisterRemoved(IAggregateRoot entity,
                          IUnitOfWorkRepository unitofWorkRepository);
    void Commit();
  }
}

