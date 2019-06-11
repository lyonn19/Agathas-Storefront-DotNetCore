using System.Collections.Generic;

using Web = Microsoft.AspNetCore.Http;
using NH = NHibernate;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Infrastructure.Querying;
using Agathas.Storefront.Repository.NHibernate.SessionStorage;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public abstract class Repository<T, TEntityKey> where T : IAggregateRoot {
    private IUnitOfWork _uow;
    private Web.IHttpContextAccessor _context;

    public Repository(IUnitOfWork uow, Web.IHttpContextAccessor context) {
      this._uow = uow;
      this._context = context;
    }

    private NH.ISession GetCurrentSession (){
      return SessionFactory.GetCurrentSession(_context);
    }

    public void Add(T entity) { GetCurrentSession().Save(entity); }
    public void Remove(T entity) { GetCurrentSession().Delete(entity); }
    public void Save(T entity) { GetCurrentSession().SaveOrUpdate(entity); }
    public T FindBy(TEntityKey id) {  return GetCurrentSession().Get<T>(id); }

    public IEnumerable<T> FindAll() {
      var criteriaQuery = GetCurrentSession().CreateCriteria(typeof(T));
      return (List<T>)criteriaQuery.List<T>();
    }

    public IEnumerable<T> FindAll(int index, int count) {
      var criteriaQuery = GetCurrentSession().CreateCriteria(typeof(T));
      return (List<T>)criteriaQuery.SetFetchSize(count)
              .SetFirstResult(index).List<T>();
    }

    public IEnumerable<T> FindBy(Query query) {
      var criteriaQuery = GetCurrentSession().CreateCriteria(typeof(T));

      AppendCriteria(criteriaQuery);

      query.TranslateIntoNHQuery<T>(criteriaQuery);
      return criteriaQuery.List<T>();
    }

    public IEnumerable<T> FindBy(Query query, int index, int count) {
      var criteriaQuery = GetCurrentSession().CreateCriteria(typeof(T));

      AppendCriteria(criteriaQuery);

      query.TranslateIntoNHQuery<T>(criteriaQuery);
      return criteriaQuery.SetFetchSize(count).SetFirstResult(index).List<T>();
    }

    public virtual void AppendCriteria(NH.ICriteria criteria) { }
  }
}
