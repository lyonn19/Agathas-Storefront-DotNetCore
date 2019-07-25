using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Shipping;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class DeliveryOptionRepository : Repository<DeliveryOption, int>,
                                                        IDeliveryOptionRepository {    
    public DeliveryOptionRepository(IUnitOfWork<IHttpContextAccessor> uow) : base(uow) { }
  }
}
