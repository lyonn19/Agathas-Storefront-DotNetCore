using System;
using System.Collections.Generic;

using Web = Microsoft.AspNetCore.Http;
using NHibernate;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Infrastructure.Querying;
using Agathas.Storefront.Infrastructure.UnitOfWork;
using Agathas.Storefront.Repository.NHibernate.SessionStorage;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public abstract class Repository<T, TEntityKey> where T : IAggregateRoot {
    private IUnitOfWork _uow;
    private readonly Web.IHttpContextAccessor _context;
    public Repository(IUnitOfWork uow, Web.IHttpContextAccessor context) {
      this._uow = uow;
      this._context = context;
    }

    public void Add(T entity) {
      SessionFactory.GetCurrentSession(_context).Save(entity);
    }

    public void Remove(T entity) {
      SessionFactory.GetCurrentSession(_context).Delete(entity);
    }

    public void Save(T entity) {
      SessionFactory.GetCurrentSession(_context).SaveOrUpdate(entity);
    }

    public T FindBy(TEntityKey id) {
      return SessionFactory.GetCurrentSession(_context).Get<T>(id);
    }

    public IEnumerable<T> FindAll() {
      ICriteria criteriaQuery =
            SessionFactory.GetCurrentSession(_context).CreateCriteria(typeof(T));

      return (List<T>)criteriaQuery.List<T>();
    }

    public IEnumerable<T> FindAll(int index, int count) {
      ICriteria criteriaQuery =
              SessionFactory.GetCurrentSession(_context).CreateCriteria(typeof(T));

      return (List<T>)criteriaQuery.SetFetchSize(count)
                            .SetFirstResult(index).List<T>();
    }

    public IEnumerable<T> FindBy(Query query) {
      ICriteria criteriaQuery =
              SessionFactory.GetCurrentSession(_context).CreateCriteria(typeof(T));

      AppendCriteria(criteriaQuery);

      query.TranslateIntoNHQuery<T>(criteriaQuery);

      return criteriaQuery.List<T>();
    }

    public IEnumerable<T> FindBy(Query query, int index, int count) {
      ICriteria criteriaQuery =
              SessionFactory.GetCurrentSession(_context).CreateCriteria(typeof(T));

      AppendCriteria(criteriaQuery);

      query.TranslateIntoNHQuery<T>(criteriaQuery);

      return criteriaQuery.SetFetchSize(count).SetFirstResult(index).List<T>();
    }

    public virtual void AppendCriteria(ICriteria criteria) { }
  }
}
