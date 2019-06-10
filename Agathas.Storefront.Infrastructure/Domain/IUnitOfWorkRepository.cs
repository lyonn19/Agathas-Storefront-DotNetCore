namespace Agathas.Storefront.Infrastructure.Domain {
  public interface IUnitOfWorkRepository {
    void PersistCreationOf(IAggregateRoot entity);
    void PersistUpdateOf(IAggregateRoot entity);
    void PersistDeletionOf(IAggregateRoot entity);
  }
}
