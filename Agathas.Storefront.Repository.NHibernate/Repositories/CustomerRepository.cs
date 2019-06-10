using System.Collections.Generic;
using System.Linq;

using NHibernate;
using NHibernate.Criterion;
using Web = Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Querying;
using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Infrastructure.Helpers;
using Agathas.Storefront.Model.Customers;
using Agathas.Storefront.Repository.NHibernate.SessionStorage;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class CustomerRepository : Repository<Customer, int>,
                                          ICustomerRepository {
    private readonly Web.IHttpContextAccessor _context;
    
    public CustomerRepository(IUnitOfWork uow, Web.IHttpContextAccessor context)
      : base(uow, context) { }

    public Customer FindBy(string identityToken) {
      ICriteria criteriaQuery = SessionFactory.GetCurrentSession(_context)
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
