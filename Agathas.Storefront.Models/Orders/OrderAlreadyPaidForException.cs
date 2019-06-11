using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agathas.Storefront.Models.Orders
{
    public class OrderAlreadyPaidForException : Exception
    {
        public OrderAlreadyPaidForException(string message)
            : base(message)
        {
        }
    }

}
