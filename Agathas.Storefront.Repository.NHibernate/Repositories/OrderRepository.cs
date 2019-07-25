using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Orders;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class OrderRepository : Repository<Order, int>, IOrderRepository {    
    public OrderRepository(IUnitOfWork<IHttpContextAccessor> uow) : base(uow) { }
  }
}
