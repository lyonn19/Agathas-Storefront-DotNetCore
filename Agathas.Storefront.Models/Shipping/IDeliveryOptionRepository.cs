using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agathas.Storefront.Infrastructure.Domain;

namespace Agathas.Storefront.Models.Shipping
{
    public interface IDeliveryOptionRepository : 
                    IReadOnlyRepository<DeliveryOption, int>
    {
    }

}
