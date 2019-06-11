using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agathas.Storefront.Models.Products;

namespace Agathas.Storefront.Models.Orders
{
    public static class OrderItemFactory
    {
        public static OrderItem CreateItemFor(Product product, Order order, int qty)
        {
            return new OrderItem(product, order, qty);
        }
    }

}
