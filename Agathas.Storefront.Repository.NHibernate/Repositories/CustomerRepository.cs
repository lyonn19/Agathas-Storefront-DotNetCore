using System.Collections.Generic;
using System.Linq;

using NHibernate;
using NHibernate.Criterion;
using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Querying;
using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Infrastructure.Helpers;
using Agathas.Storefront.Models.Customers;
using Agathas.Storefront.Repository.NHibernate.SessionStorage;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class CustomerRepository : Repository<Customer, int>,
                                          ICustomerRepository {    
    public CustomerRepository(IUnitOfWork<IHttpContextAccessor> uow) : base(uow) { }

    public Customer FindBy(string identityToken) {
      ICriteria criteriaQuery = SessionFactory.GetCurrentSession(base._uow.Context)
                .CreateCriteria(typeof(Customer))
                .Add(Expression.Eq(PropertyNameHelper
                .ResolvePropertyName<Customer>
              (c => c.IdentityToken), identityToken));

      IList<Customer> customers = criteriaQuery.List<Customer>();

      Customer customer = customers.FirstOrDefault();
      return customer;
    }
  }
}
