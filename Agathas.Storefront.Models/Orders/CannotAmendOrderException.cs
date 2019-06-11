using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agathas.Storefront.Models.Orders
{
    public class CannotAmendOrderException : Exception
    {
        public CannotAmendOrderException(string message)
            : base(message)
        {

        }
    }

}
