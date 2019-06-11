using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agathas.Storefront.Models.Shipping
{
    public interface IDeliveryOption
    {
        int Id { get; set; }
        decimal FreeDeliveryThreshold { get; }
        decimal Cost { get; }
        ShippingService ShippingService { get; }
        decimal GetDeliveryChargeForBasketTotalOf(decimal total);
    }
}
