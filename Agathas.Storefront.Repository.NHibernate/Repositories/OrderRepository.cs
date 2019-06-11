using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Orders;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class OrderRepository : Repository<Order, int>, IOrderRepository {
    private readonly IHttpContextAccessor _context;
    
    public OrderRepository(IUnitOfWork uow, IHttpContextAccessor context)
      : base(uow, context) { }
  }
}
