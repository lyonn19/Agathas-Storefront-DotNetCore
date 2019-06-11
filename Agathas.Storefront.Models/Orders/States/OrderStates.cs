using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agathas.Storefront.Models.Orders.States
{
    public class OrderStates
    {
        public static readonly IOrderState Open =
                      new OpenOrderState() { Id = 1 };
        public static readonly IOrderState Submitted =
                      new SubmittedOrderState() { Id = 2 };
    }

}
